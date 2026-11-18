// [과제 변경] 과제1의 비즈니스 로직(PokerCore 폴더의 파일 9개)을 그대로 사용
using PokerCore;

namespace G26W10WPFCardDealer;

// [과제 변경] 수업 코드의 int 기반 record Card(int Suit, int Value) 삭제
//   - PokerCore 에도 같은 이름의 Card 가 있어서 이름이 충돌함
//     (같은 네임스페이스 안의 Card 가 using PokerCore 보다 우선 → card.Rank 를 찾지 못함)
//   - 족보 판별에는 Rank·Suit 가 enum 으로 된 PokerCore.Card 가 더 적합하므로 하나로 통일

/// <summary>
/// Model : 카드를 섞고 나눠 주는 일(Deck)과 족보 판별(FiveCardHandEvaluator)은 PokerCore 가 하고,
/// CardModel 은 "현재 5장"과 "판별 결과"만 들고 있음. 화면(WPF)에 대해서는 전혀 모름
/// </summary>
public class CardModel
{
    // [과제 변경] 카드 수 5 를 PokerCore 의 상수에서 가져옴
    public static readonly int NUMBER_OF_CARDS = FiveCardHandEvaluator.HandSize;

    // [과제 변경] 덱과 족보 판별기를 PokerCore 에서 가져와 사용
    private readonly Deck _deck = new();
    private readonly IHandEvaluator _evaluator = new FiveCardHandEvaluator();

    // [과제 변경] List<Card> → IReadOnlyList<PokerCore.Card>, 분배 전에는 빈 목록
    public IReadOnlyList<Card> Cards { get; private set; } = [];

    // [과제 변경] 현재 패의 족보 판별 결과 추가 (분배 전에는 null)
    public HandResult? Result { get; private set; }

    // [과제 변경] 0~51 int 셔플 → PokerCore.Deck 으로 분배하고 족보 판별 결과를 돌려줌
    public HandResult DealCards()
    {
        _deck.Reset();                                       // 52장 채우고 섞기 (Fisher–Yates)
        IReadOnlyList<Card> hand = _deck.Deal(NUMBER_OF_CARDS);   // 위에서 5장 → 중복 불가능

        HandResult result = _evaluator.Evaluate(hand);       // 족보 판별
        Result = result;
        Cards = result.Cards;                                // 높은 숫자 순으로 정렬된 5장
        return result;
    }
}
