using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PromptSystem
{
    public class Notice
    {
        public RectTransform Status;
        public Vector2 Position;
    }

    public class Prompt : MonoBehaviour
    {
        [SerializeField] private GameObject Status;
        [SerializeField] private Sprite Reload;//重载
        [SerializeField] private Sprite Saving;//保存中
        [SerializeField] private Sprite Saved;//已保存
        [SerializeField] private Sprite Failed;//失败
        //可以自行添加其他通知

        private List<Notice> NoticeList = new List<Notice>();
        private List<Notice> DestroyList = new List<Notice>();

        public void ShowStatus(int type)
        {
            Sprite sprite = type switch
            {
                0 => Reload, // 重载
                1 => Saving, // 保存中
                2 => Saved,  // 已保存
                3 => Failed, // 失败
                //这里也要同步添加
                _ => Failed // 默认情况
            };
            CreateNotice(sprite);
        }

        private void CreateNotice(Sprite sprite)
        {
            GameObject statusInstance = Instantiate(Status, transform);
            statusInstance.GetComponent<Image>().sprite = sprite;

            Notice notice = new Notice
            {
                Status = statusInstance.GetComponent<RectTransform>(),
                Position = new Vector2(0, -50 - 100 * NoticeList.Count)
            };

            NoticeList.Add(notice);
            notice.Status.anchoredPosition = notice.Position + new Vector2(-300, 0);

            // 延迟移动和销毁
            MoveNotice(notice, 2000).Forget();
            DestroyNotice(notice, 2300).Forget();
        }

        private async UniTaskVoid MoveNotice(Notice notice, int delayMilliseconds)
        {
            await UniTask.Delay(delayMilliseconds);

            NoticeList.Remove(notice);
            DestroyList.Add(notice);

            notice.Position.x = -300;

            //更新剩余通知位置
            foreach (Notice Notice in NoticeList)
            {
                Notice.Position += new Vector2(0, 100);
            }
            foreach (Notice Notice in DestroyList)
            {
                Notice.Position += new Vector2(0, 100);
            }
        }

        private async UniTaskVoid DestroyNotice(Notice notice, int delayMilliseconds)
        {
            await UniTask.Delay(delayMilliseconds);

            DestroyList.Remove(notice);
            Destroy(notice.Status.gameObject);
        }

        private void FixedUpdate()
        {
            // 平滑移动所有通知到目标位置
            foreach (Notice notice in NoticeList)
            {
                Vector2 currentPosition = notice.Status.anchoredPosition;
                Vector2 newPosition = Vector2.Lerp(currentPosition, notice.Position, 0.2f);
                notice.Status.anchoredPosition = newPosition;
            }
            foreach (Notice notice in DestroyList)
            {
                Vector2 currentPosition = notice.Status.anchoredPosition;
                Vector2 newPosition = Vector2.Lerp(currentPosition, notice.Position, 0.2f);
                notice.Status.anchoredPosition = newPosition;
            }
        }
    }
}