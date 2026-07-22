using LSR_Engine.src.Common;
using LSR_Engine.src.Common.Interface;
using System;
using System.Collections.Generic;

namespace LSR_Engine.src.Input
{
    /// <summary>
    /// 1フレーム分の入力状態を表す不変のデータ構造
    /// </summary>
    internal readonly struct ActionState
    {
        public bool IsDown { get; }
        public bool IsTriggered { get; }
        public bool IsReleased { get; }
        public int HeldFrames { get; }

        public ActionState(bool isDown, bool isTriggered, bool isReleased, int heldFrames)
        {
            IsDown = isDown;
            IsTriggered = isTriggered;
            IsReleased = isReleased;
            HeldFrames = heldFrames;
        }
    }

    internal class InputBuffer : IUpdatable
    {
        // 外部から生の入力を受けるバッファ
        private readonly HashSet<Actions> _rawInput = new HashSet<Actions>();
        // 現在のフレームで確定した状態
        private readonly HashSet<Actions> _currentFrameDown = new HashSet<Actions>();
        // 1フレーム前の状態
        private readonly HashSet<Actions> _previousFrameDown = new HashSet<Actions>();
        private readonly Dictionary<Actions, int> _heldFrames = new Dictionary<Actions, int>();

        // GC対策: Enum配列をキャッシュ（配列のアロケーションを防止）
        private static readonly Actions[] AllActions = (Actions[])Enum.GetValues(typeof(Actions));

        /// <summary>
        /// 入力イベントが発生したタイミングに呼び出す。
        /// </summary>
        /// <param name="action">入力され、発生したアクションの名称</param>
        /// <param name="isDown">アクションが押されているかどうか</param>
        public void SetActionState(Actions action, bool isDown)
        {
            if (isDown) _rawInput.Add(action);
            else _rawInput.Remove(action);
        }

        /// <summary>
        /// 毎フレーム呼び出され、状態を確定させる
        /// </summary>
        public void Tick()
        {
            // 1. 現在のフレーム状態を過去状態へシフト
            _previousFrameDown.Clear();
            foreach (Actions action in _currentFrameDown)
            {
                _previousFrameDown.Add(action);
            }

            // 2. 生入力バッファから現在フレームの状態をコピー
            _currentFrameDown.Clear();
            foreach (Actions action in _rawInput)
            {
                _currentFrameDown.Add(action);
            }

            // 3. 桜花フレーム数の更新（キャッシュした配列を使用）
            foreach (Actions action in AllActions)
            {
                if (_currentFrameDown.Contains(action))
                {
                    _heldFrames[action] = _heldFrames.GetValueOrDefault(action, 0) + 1;
                }
                else
                {
                     _heldFrames[action] = 0;
                }
            }
        }

        /// <summary>
        /// 現フレームで Down または Triggered になっているアクションとその状態を返す
        /// </summary>
        public IEnumerable<(Actions Action, ActionState State)> GetActiveActions()
        {
            foreach (Actions action in AllActions)
            {
                var state = GetActionState(action);

                // 何らかの入力があるアクションだけを返す
                if (state.IsDown || state.IsTriggered)
                {
                    yield return (action, state);
                }
            }
        }

        private ActionState GetActionState(Actions action)
        {
            bool isDown = _currentFrameDown.Contains(action);
            bool wasDown = _previousFrameDown.Contains(action);

            return new ActionState(
                isDown,
                isDown && !wasDown,
                !isDown && wasDown,
                _heldFrames.GetValueOrDefault(action, 0)
            );
        }
    }
}
