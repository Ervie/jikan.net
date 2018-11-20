Imports JikanDotNet

Module Program

    Sub Main(args As String())
        Dim jikan As Jikan

        ' Initialize JikanWrapper
        jikan = New Jikan(True)

        ' Send request for "Cowboy Bebop" anime
        Dim cowboyBebop = jikan.GetAnime(1).Result

        ' Output -> "Cowboy Bebop"
        Console.WriteLine(cowboyBebop.Title)
        ' Output -> "TV"
        Console.WriteLine(cowboyBebop.Type)
        'Output -> "R - 17+ (violence & profanity)"
        Console.WriteLine(cowboyBebop.Rating)


        ' Send request for "Berserk" manga
        Dim berserkManga = jikan.GetManga(2).Result

        ' Output -> "Berserk"
        Console.WriteLine(berserkManga.Title)
        ' Output -> "Publishing"
        Console.WriteLine(berserkManga.Status)

        ' Send request for Hayao Miyazaki
        Dim hayaoMiyazaki = jikan.GetPerson(1870).Result

        ' List Miyazaki anime on output
        For Each staffPosition In hayaoMiyazaki.AnimeStaffPositions
            Console.WriteLine("Anime: " + staffPosition.Anime.Name + ", role: " + staffPosition.Position)
        Next


        ' Send request for Lain Iwakura
        Dim lainIwakura = jikan.GetCharacter(2219).Result

        ' List Lain's voice actresses with their respective languages
        For Each voiceActor In lainIwakura.VoiceActors
            Console.WriteLine("Name: " + voiceActor.Name + ", language: " + voiceActor.Language)
        Next

        ' List all anime in which Lain appeared
        For Each anime In lainIwakura.Animeography
            Console.WriteLine("Title: " + anime.Name)
        Next

    End Sub

End Module