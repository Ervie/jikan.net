using System;
using System.Collections.Generic;
using System.Text;

namespace JikanDotNet
{
    interface IJikan
    {
		void GetPerson(long id);

		void GetAnime(long id);

		void GetManga(long id);

		void GetCharacter(long id);
    }
}
