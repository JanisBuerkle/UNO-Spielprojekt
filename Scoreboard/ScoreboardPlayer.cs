using System.Runtime.Serialization;

namespace UNO_Spielprojekt.Scoreboard;

[DataContract(Name = "ScoreboardPlayer", Namespace = "http://contoso.com")]
public class ScoreboardPlayer
{
    [DataMember]
    public string PlayerScoreboardName { get; set; }
    [DataMember]
    public int PlayerScoreboardScore { get; set; }
}