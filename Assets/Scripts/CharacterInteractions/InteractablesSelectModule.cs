using System.Collections.Generic;

namespace CharacterInteractions
{
    public class InteractablesSelectModule
    {
        private readonly List<IInteractable> _interactables = new();
        private IInteractable _interactable;

        public IInteractable Interactable => _interactable;
        
        public void OnInteractableLeaveArea(IInteractable obj)
        {
            _interactables.Remove(obj);
            _interactable = _interactables.Count > 0 ? _interactables[0] : null;
        }

        public void OnInteractableEnterArea(IInteractable obj)
        {
            if (_interactables.Contains(obj))
                return;
            
            _interactables.Add(obj);
            if (_interactable == null || IsNewestCloser(obj))
                _interactable = obj;
        }

        private bool IsNewestCloser(IInteractable interactable)
        {
            return _interactable.Transform.position.x < interactable.Transform.position.x;
        }
    }
}