﻿Imports System.IO
Imports System.Xml

Public Class RSSFeed

    ''' <summary>
    ''' A URL to RSS feed
    ''' </summary>
    Public ReadOnly Property FeedUrl As String
    ''' <summary>
    ''' RSS feed type
    ''' </summary>
    Public ReadOnly Property FeedType As RSSFeedType
    ''' <summary>
    ''' RSS feed type
    ''' </summary>
    Public ReadOnly Property FeedTitle As String
    ''' <summary>
    ''' RSS feed description (Atom feeds not supported and always return an empty string)
    ''' </summary>
    Public ReadOnly Property FeedDescription As String
    ''' <summary>
    ''' Feed articles
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FeedArticles As List(Of RSSArticle)

    ''' <summary>
    ''' makes a new instance of an RSS feed class
    ''' </summary>
    ''' <param name="FeedUrl">A URL to RSS feed</param>
    ''' <param name="FeedType">A feed type to parse. If set to Infer, it will automatically detect the type based on contents.</param>
    Public Sub New(ByVal FeedUrl As String, ByVal FeedType As RSSFeedType)
        'Load an RSS feed from URL
        Dim FeedWebRequest As HttpWebRequest = DirectCast(WebRequest.Create(FeedUrl), HttpWebRequest)
        Dim FeedWebResponse As WebResponse = FeedWebRequest.GetResponse()
        Dim FeedStream As Stream = FeedWebResponse.GetResponseStream()
        Dim FeedDocument As New XmlDocument
        FeedDocument.Load(FeedStream)

        'Infer feed type
        Dim FeedNodeList As XmlNodeList
        If FeedType = RSSFeedType.Infer Then
            If FeedDocument.GetElementsByTagName("rss").Count <> 0 Then
                FeedNodeList = FeedDocument.GetElementsByTagName("rss")
                FeedType = RSSFeedType.RSS2
            ElseIf FeedDocument.GetElementsByTagName("rdf:RDF").Count <> 0 Then
                FeedNodeList = FeedDocument.GetElementsByTagName("rdf:RDF")
                FeedType = RSSFeedType.RSS1
            ElseIf FeedDocument.GetElementsByTagName("feed").Count <> 0 Then
                FeedNodeList = FeedDocument.GetElementsByTagName("feed")
                FeedType = RSSFeedType.Atom
            End If
        ElseIf FeedType = RSSFeedType.RSS2 Then
            FeedNodeList = FeedDocument.GetElementsByTagName("rss")
            If FeedNodeList.Count = 0 Then Throw New Exceptions.InvalidFeedTypeException(DoTranslation("Invalid RSS2 feed."))
        ElseIf FeedType = RSSFeedType.RSS1 Then
            FeedNodeList = FeedDocument.GetElementsByTagName("rdf:RDF")
            If FeedNodeList.Count = 0 Then Throw New Exceptions.InvalidFeedTypeException(DoTranslation("Invalid RSS1 feed."))
        ElseIf FeedType = RSSFeedType.Atom Then
            FeedNodeList = FeedDocument.GetElementsByTagName("feed")
            If FeedNodeList.Count = 0 Then Throw New Exceptions.InvalidFeedTypeException(DoTranslation("Invalid Atom feed."))
        End If

        'Populate basic feed properties
        Dim FeedTitle As String = GetFeedProperty("title", FeedNodeList, FeedType)
        Dim FeedDescription As String = GetFeedProperty("description", FeedNodeList, FeedType)

        'Populate articles
        Dim Articles As List(Of RSSArticle) = MakeRssArticlesFromFeed(FeedNodeList, FeedType)

        'Install the variables to a new instance
        Me.FeedUrl = FeedUrl
        Me.FeedType = FeedType
        Me.FeedTitle = FeedTitle
        Me.FeedDescription = FeedDescription
        Me.FeedArticles = Articles
    End Sub

End Class

''' <summary>
''' Enumeration for RSS feed type
''' </summary>
Public Enum RSSFeedType
    ''' <summary>
    ''' The RSS format is RSS 2.0
    ''' </summary>
    RSS2
    ''' <summary>
    ''' The RSS format is RSS 1.0
    ''' </summary>
    RSS1
    ''' <summary>
    ''' The RSS format is Atom
    ''' </summary>
    Atom
    ''' <summary>
    ''' Try to infer RSS type
    ''' </summary>
    Infer = 1024
End Enum
