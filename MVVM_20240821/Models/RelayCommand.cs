using System;
using System.Windows.Input;

namespace MVVM_20240821.Models
{
    /// <summary>
    /// 제네릭 명령을 나타내는 클래스입니다. MVVM 패턴에서 ViewModel과 View를 연결하는 데 사용됩니다.
    /// 이 클래스는 <see cref="ICommand"/> 인터페이스를 구현하여 명령 실행 및 실행 가능 여부를 결정합니다.
    /// </summary>
    /// <typeparam name="T">명령에 전달할 매개변수의 유형을 나타냅니다.</typeparam>
    public class RelayCommand<T> : ICommand
    {
        // 명령이 실행될 때 호출되는 메서드를 참조합니다.
        private readonly Action<T> execute;

        // 명령이 실행 가능한지 여부를 결정하는 메서드를 참조합니다.
        private readonly Predicate<T> canExecute;

        /// <summary>
        /// 명령의 실행 가능 상태가 변경될 때 발생합니다.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// <see cref="RelayCommand{T}"/> 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="execute">명령이 실행될 때 호출되는 메서드입니다. 이 매개변수는 null이 될 수 없습니다.</param>
        /// <exception cref="ArgumentNullException">execute 매개변수가 null인 경우 발생합니다.</exception>
        public RelayCommand(Action<T> execute) : this(execute, null) { }

        /// <summary>
        /// <see cref="RelayCommand{T}"/> 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="execute">명령이 실행될 때 호출되는 메서드입니다. 이 매개변수는 null이 될 수 없습니다.</param>
        /// <param name="canExecute">명령이 실행 가능한지 여부를 결정하는 메서드입니다. 이 매개변수는 null이 될 수 있습니다.</param>
        /// <exception cref="ArgumentNullException">execute 매개변수가 null인 경우 발생합니다.</exception>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            // execute 매개변수가 null이면 ArgumentNullException을 발생시킵니다.
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        /// <summary>
        /// 명령이 실행 가능한지 여부를 결정합니다.
        /// </summary>
        /// <param name="parameter">명령에 전달될 매개변수입니다.</param>
        /// <returns>명령이 실행 가능한 경우 true, 그렇지 않은 경우 false를 반환합니다.</returns>
        public bool CanExecute(object parameter) => canExecute?.Invoke((T)parameter) ?? true;

        /// <summary>
        /// 명령을 실행합니다.
        /// </summary>
        /// <param name="parameter">명령에 전달될 매개변수입니다.</param>
        public void Execute(object parameter) => execute((T)parameter);
    }
}
