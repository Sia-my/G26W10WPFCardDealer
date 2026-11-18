using CommunityToolkit.Mvvm.ComponentModel;
using PokerCore;

namespace G26W10WPFCardDealer;

// [과제 변경] 새 파일 : 확률표 한 줄 (ViewModel 쪽 데이터)
//   PokerCore.HandProbability 는 불변(record)이라 "현재 패인지" 표시를 넣을 수 없으므로,
//   화면에 필요한 문자열과 IsCurrent(강조 여부)를 가진 행 객체로 한 번 감쌈
public partial class HandProbabilityRow(HandProbability probability) : ObservableObject
{
    public HandRank Rank { get; } = probability.Rank;
    public string Name { get; } = probability.Rank.ToKoreanName();     // 예) 풀하우스
    public string CountText { get; } = $"{probability.Count:N0}";       // 예) 3,744
    public string ProbabilityText { get; } = $"{probability.Probability:P4}";   // 예) 0.1441%

    // 현재 패의 족보이면 true → XAML 의 DataTrigger 가 행을 강조
    [ObservableProperty]
    private bool _isCurrent;
}
