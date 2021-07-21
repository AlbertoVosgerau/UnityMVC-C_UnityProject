using UnityEngine;

namespace Implementations
{
    public class InputImpl : IInput
    {
        public bool ChangeColor => Input.GetKeyDown(KeyCode.Space);
    }
}
