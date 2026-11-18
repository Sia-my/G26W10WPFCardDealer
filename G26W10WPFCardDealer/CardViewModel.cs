using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
// [과제 변경] PokerCore 의 HandResult, HandProbabilityTable 사용
using PokerCore;
using System.Collections.ObjectModel;

namespace G26W10WPFCardDealer;

public partial class CardViewModel(
    CardModel model
) : ObservableObject
{
    // [과제 변경] 조커 이미지 경로를 CardImageMapper 상수로 옮김
    [ObservableProperty]
    private ObservableCollection<string> _cardResource = [..
        Enumerable.Repeat(CardImageMapper.JokerImage, CardModel.NUMBER_OF_CARDS)
    ];

    // [과제 변경] 족보 설명 (예: K·9 투페어) Property 추가
    [ObservableProperty]
    private string _resultText = "[카드 분배] 버튼을 눌러 주세요";

    // [과제 변경] 카드 목록 + 족보 이름 + 확률 Property 추가
    [ObservableProperty]
    private string _infoText = "카드 : -     |     족보 : -";

    // [과제 변경] 전체 족보 확률표 (PokerCore.HandProbabilityTable 을 그대로 사용)
    public ObservableCollection<HandProbabilityRow> Probabilities { get; } = [..
        HandProbabilityTable.All.Select(p => new HandProbabilityRow(p))
    ];

    // [과제 변경] 확률표 아래 합계 (52C5)
    public string TotalText { get; } = $"합계 : {HandProbabilityTable.TotalHands:N0} 가지 (52C5)";

    [RelayCommand]
    public void DealCards()
    {
        // [과제 변경] 분배 + 족보 판별 결과를 받음
        HandResult result = model.DealCards();

        for (int i = 0; i < model.Cards.Count; i++)
        {
            // [과제 변경] GetCardImageName(int 기반) → CardImageMapper.ToImagePath(PokerCore.Card)
            CardResource[i] = CardImageMapper.ToImagePath(model.Cards[i]);
        }

        // [과제 변경] 족보 결과를 화면에 보여 줄 문자열로 변환
        HandProbability probability = HandProbabilityTable.Of(result.Category);
        ResultText = result.Description;
        InfoText = $"카드 : {string.Join("  ", model.Cards)}     |     족보 : {result.Name}" +
                   $"     |     이 족보가 나올 확률 : {probability.Probability:P4}";

        // [과제 변경] 확률표에서 현재 족보만 강조
        foreach (HandProbabilityRow row in Probabilities)
        {
            row.IsCurrent = row.Rank == result.Category;
        }
    }

    // [과제 변경] 수업 코드의 GetCardImageName(Card) 삭제 → CardImageMapper.cs 로 분리
}
