using PokerCore;

namespace G26W10WPFCardDealer;

// [과제 변경] 새 파일 : PokerCore.Card → WPF 이미지 경로 문자열 변환 (View 전용)
//   과제1(WinForms)의 CardImageMapper 는 System.Drawing.Image 와 Properties.Resources 를 써서 WPF 에서 쓸 수 없음
//   → 파일 이름 규칙은 같고, WPF 는 Image.Source 에 "Images/xxx.png" 문자열을 Binding 하면 되므로 경로만 만들어 줌
//   - 파일 이름 : {숫자}_of_{무늬}.png  예) ace_of_spades.png, 10_of_hearts.png
//   - J, Q, K 는 그림이 있는 버전(이름 끝에 "2")을 사용  예) king_of_clubs2.png
public static class CardImageMapper
{
    public const string JokerImage = "Images/black_joker.png";

    public static string ToImagePath(Card card)
    {
        string rank = card.Rank switch
        {
            Rank.Ace => "ace",
            Rank.King => "king",
            Rank.Queen => "queen",
            Rank.Jack => "jack",
            _ => ((int)card.Rank).ToString(),                // 2 ~ 10
        };
        string suit = card.Suit.ToString().ToLowerInvariant();   // Spades → spades
        bool isFaceCard = card.Rank is Rank.Jack or Rank.Queen or Rank.King;

        return $"Images/{rank}_of_{suit}{(isFaceCard ? "2" : "")}.png";
    }
}
