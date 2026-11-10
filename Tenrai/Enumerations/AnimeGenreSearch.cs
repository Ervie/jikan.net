using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Enumeration of accessible genres (search config).
/// </summary>
public enum AnimeGenreSearch
{
	/// <summary>
	/// Action.
	/// </summary>
	[Description("1")]
	Action,

	/// <summary>
	/// Adventure.
	/// </summary>
	[Description("2")]
	Adventure,

	/// <summary>
	/// Cars.
	/// </summary>
	[Description("3")]
	Cars,

	/// <summary>
	/// Comedy.
	/// </summary>
	[Description("4")]
	Comedy,

	/// <summary>
	/// Avante garde.
	/// </summary>
	[Description("5")]
	AvanteGarde,

	/// <summary>
	/// Demons.
	/// </summary>
	[Description("6")]
	Demons,

	/// <summary>
	/// Mystery.
	/// </summary>
	[Description("7")]
	Mystery,

	/// <summary>
	/// Drama.
	/// </summary>
	[Description("8")]
	Drama,

	/// <summary>
	/// Ecchi.
	/// </summary>
	[Description("9")]
	Ecchi,

	/// <summary>
	/// Fantasy.
	/// </summary>
	[Description("10")]
	Fantasy,

	/// <summary>
	/// Game.
	/// </summary>
	[Description("11")]
	Game,

	/// <summary>
	/// Hentai.
	/// </summary>
	[Description("12")]
	Hentai,

	/// <summary>
	/// Historical.
	/// </summary>
	[Description("13")]
	Historical,

	/// <summary>
	/// Horror.
	/// </summary>
	[Description("14")]
	Horror,

	/// <summary>
	/// Kids.
	/// </summary>
	[Description("15")]
	Kids,

	/// <summary>
	/// Martial arts.
	/// </summary>
	[Description("17")]
	MartialArts,

	/// <summary>
	/// Mecha.
	/// </summary>
	[Description("18")]
	Mecha,

	/// <summary>
	/// Music.
	/// </summary>
	[Description("19")]
	Music,

	/// <summary>
	/// Parody.
	/// </summary>
	[Description("20")]
	Parody,

	/// <summary>
	/// Samurai.
	/// </summary>
	[Description("21")]
	Samurai,

	/// <summary>
	/// Romance.
	/// </summary>
	[Description("22")]
	Romance,

	/// <summary>
	/// School.
	/// </summary>
	[Description("23")]
	School,

	/// <summary>
	/// SciFi.
	/// </summary>
	[Description("24")]
	SciFi,

	/// <summary>
	/// Shoujo.
	/// </summary>
	[Description("25")]
	Shoujo,

	/// <summary>
	/// Girls love.
	/// </summary>
	[Description("26")]
	GirlsLove,

	/// <summary>
	/// Shounen.
	/// </summary>
	[Description("27")]
	Shounen,

	/// <summary>
	/// Boys love.
	/// </summary>
	[Description("28")]
	BoysLove,

	/// <summary>
	/// Space.
	/// </summary>
	[Description("29")]
	Space,

	/// <summary>
	/// Sports.
	/// </summary>
	[Description("30")]
	Sports,

	/// <summary>
	/// SuperPower.
	/// </summary>
	[Description("31")]
	SuperPower,

	/// <summary>
	/// Vampire.
	/// </summary>
	[Description("32")]
	Vampire,

	/// <summary>
	/// Harem.
	/// </summary>
	[Description("35")]
	Harem,

	/// <summary>
	/// Slice of life.
	/// </summary>
	[Description("36")]
	SliceOfLife,

	/// <summary>
	/// Supernatural.
	/// </summary>
	[Description("37")]
	Supernatural,

	/// <summary>
	/// Military.
	/// </summary>
	[Description("38")]
	Military,

	/// <summary>
	/// Police.
	/// </summary>
	[Description("39")]
	Police,

	/// <summary>
	/// Psychological.
	/// </summary>
	[Description("40")]
	Psychological,

	/// <summary>
	/// Suspense.
	/// </summary>
	[Description("41")]
	Suspense,

	/// <summary>
	/// Seinen.
	/// </summary>
	[Description("42")]
	Seinen,

	/// <summary>
	/// Josei.
	/// </summary>
	[Description("43")]
	Josei,

	/// <summary>
	/// Award winning.
	/// </summary>
	[Description("46")]
	AwardWinning,

	/// <summary>
	/// Gourmet.
	/// </summary>
	[Description("47")]
	Gourmet,

	/// <summary>
	/// Work life.
	/// </summary>
	[Description("48")]
	WorkLife,

	/// <summary>
	/// Erotica.
	/// </summary>
	[Description("49")]
	Erotica,

	/// <summary>
	/// Adult cast.
	/// </summary>
	[Description("50")]
	AdultCast,

	/// <summary>
	/// Anthropomorphic.
	/// </summary>
	[Description("51")]
	Anthropomorphic,

	/// <summary>
	/// CGDCT (cute girls doing cute things).
	/// </summary>
	[Description("52")]
	Cgdct,

	/// <summary>
	/// Childcare.
	/// </summary>
	[Description("53")]
	Childcare,

	/// <summary>
	/// Combat sports.
	/// </summary>
	[Description("54")]
	CombatSports,

	/// <summary>
	/// Delinquents.
	/// </summary>
	[Description("55")]
	Delinquents,

	/// <summary>
	/// Educational.
	/// </summary>
	[Description("56")]
	Educational,

	/// <summary>
	/// Gag humor.
	/// </summary>
	[Description("57")]
	GagHumor,

	/// <summary>
	/// Gore.
	/// </summary>
	[Description("58")]
	Gore,

	/// <summary>
	/// High stakes game.
	/// </summary>
	[Description("59")]
	HighStakesGame,

	/// <summary>
	/// Idols (female).
	/// </summary>
	[Description("60")]
	IdolsFemale,

	/// <summary>
	/// Idols (male).
	/// </summary>
	[Description("61")]
	IdolsMale,

	/// <summary>
	/// Isekai.
	/// </summary>
	[Description("62")]
	Isekai,

	/// <summary>
	/// Iyashikei.
	/// </summary>
	[Description("63")]
	Iyashikei,

	/// <summary>
	/// Love polygon.
	/// </summary>
	[Description("64")]
	LovePolygon,

	/// <summary>
	/// Magical sex shift.
	/// </summary>
	[Description("65")]
	MagicalSexShift,

	/// <summary>
	/// Mahou shoujo.
	/// </summary>
	[Description("66")]
	MahouShoujo,

	/// <summary>
	/// Medical.
	/// </summary>
	[Description("67")]
	Medical,

	/// <summary>
	/// Organized crime.
	/// </summary>
	[Description("68")]
	OrganizedCrime,

	/// <summary>
	/// Otaku culture.
	/// </summary>
	[Description("69")]
	OtakuCulture,

	/// <summary>
	/// Performing arts.
	/// </summary>
	[Description("70")]
	PerformingArts,

	/// <summary>
	/// Pets.
	/// </summary>
	[Description("71")]
	Pets,

	/// <summary>
	/// Reincarnation.
	/// </summary>
	[Description("72")]
	Reincarnation,

	/// <summary>
	/// Reverse harem.
	/// </summary>
	[Description("73")]
	ReverseHarem,

	/// <summary>
	/// Love status quo.
	/// </summary>
	[Description("74")]
	LoveStatusQuo,

	/// <summary>
	/// Showbiz.
	/// </summary>
	[Description("75")]
	Showbiz,

	/// <summary>
	/// Survival.
	/// </summary>
	[Description("76")]
	Survival,

	/// <summary>
	/// Team sports.
	/// </summary>
	[Description("77")]
	TeamSports,

	/// <summary>
	/// Time travel.
	/// </summary>
	[Description("78")]
	TimeTravel,

	/// <summary>
	/// Video game.
	/// </summary>
	[Description("79")]
	VideoGame,

	/// <summary>
	/// Visual arts.
	/// </summary>
	[Description("80")]
	VisualArts,

	/// <summary>
	/// Crossdressing.
	/// </summary>
	[Description("81")]
	Crossdressing,

	/// <summary>
	/// Urban fantasy.
	/// </summary>
	[Description("82")]
	UrbanFantasy,

	/// <summary>
	/// Villainess.
	/// </summary>
	[Description("83")]
	Villainess
}