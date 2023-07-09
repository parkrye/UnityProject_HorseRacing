using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// 특정 CSW 파일을 읽고 쓰는 스크립트
/// </summary>
public static class CSVRW
{
    /// <summary>
    /// 파일을 읽는 메소드
    /// </summary>
    /// <param name="fileName">파일 이름</param>
    /// <returns>딕셔너리 구조의 기록 파일</returns>
    public static Dictionary<string, int> ReadCSV(string fileName)
    {
        Dictionary<string, int> answer = new();         // 저장할 딕셔너리

        TextAsset data = GameManager.Resource.Load<Object>($"CSV/{fileName}") as TextAsset;
                                                        // 텍스트에셋으로 변환한 기록 파일 데이터
        string[] texts = data.text.Split("\n");         // 데이터를 줄바꿈 단위로 분할한 문자열 배열

        for (int i = 0; i < texts.Length; i++)          // 각 문자열 요소에 대하여
        {
            if (texts[i].Length <= 1)                   // 길이가 1 이하라면 즉시 종료
                break;                                  // (저장 방식 문제로 기록 데이터에 빈 문자열 한 줄이 추가되기 때문)
            string[] line = texts[i].Split(",");        // 반점으로 분할한 두 문자열을
            answer.Add(line[0], int.Parse(line[1]));    // 키와 값으로 저장
        }

        return answer;                                  // 저장한 딕셔너리를 반환
    }

    /// <summary>
    /// 기록 파일을 저장하는 메소드
    /// </summary>
    public static void WriteCSV(string fileName, Dictionary<string, int> data)
    {
        StringBuilder sb = new();                           // 저장할 스트링빌더
        string delimiter = ",";                             // 구분자
        foreach(KeyValuePair<string, int> pair in data)     // 각 데이터 쌍에 대하여
        {
            // 키, 구분자, 값을 저장
            sb.Append(pair.Key);
            sb.Append(delimiter);
            sb.AppendLine(pair.Value.ToString());
        }
        Stream fileStream = new FileStream($"Assets/Resources/CSV/{fileName}.csv", FileMode.Create, FileAccess.Write);
                                                                    // 저장할 주소, 파일은 쓰거나 새로 생성
        StreamWriter outStream = new(fileStream, Encoding.UTF8);    // 출력 형식
        outStream.WriteLine(sb);                                    // 스트링빌더를 쓰고
        outStream.Close();                                          // 출력 종료
    }
}
