// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Control/Land/LandControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @LandControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @LandControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""LandControls"",
    ""maps"": [
        {
            ""name"": ""LandMovement"",
            ""id"": ""081671f7-1952-4cf5-87dc-fd0a8e9f9ca1"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""68f3e4c6-8820-433a-8cba-7cee40060f57"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""3755434c-c3b6-4266-b336-d747c97e2c5c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Embody"",
                    ""type"": ""Button"",
                    ""id"": ""3ddb0534-f699-4c81-809b-48c14b75ae1d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""7691949e-e400-416c-b146-02033bb2c310"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Special"",
                    ""type"": ""Button"",
                    ""id"": ""2317b862-0d89-40d5-8612-50bcc609c10f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""a617b517-1265-4bc7-8844-ba4dfbf36b64"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ca0d764b-c1f2-43cd-961c-df83a5b07095"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56c0104e-326f-4790-bfd1-e86f677670ee"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""863a8078-ed72-48a7-ae15-79258864378e"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Embody"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cea5484b-1561-40aa-b16f-64a37db2b8da"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Embody"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""MovementController"",
                    ""id"": ""422fff57-09cb-460b-9b6f-1f4179bc1fdf"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""492c2024-c7ca-478e-b060-6a416d1a2156"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""56dd8094-5832-470d-a14a-0c925e7a4042"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Movement"",
                    ""id"": ""9a88ee09-6c37-4208-957e-a61bd215f10f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c0c621a4-fbbd-46b7-9435-39a36cfd09e2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0737a925-aa8f-4764-aed9-819f9b1b3336"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8aad14b5-ef1d-4ce6-89f8-49b88edc40a2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f66f056e-d6e9-4ce3-8507-7a6266182a86"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82a40bca-bef8-4482-bdd9-a53e227989ea"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aee032f7-cf75-4f98-bf5f-0c520fbb9fce"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7fd18050-93c8-4342-a12d-a9cfda31f3cc"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8dd41f0-3c0c-4a0f-b2c9-d93718d58d48"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // LandMovement
        m_LandMovement = asset.FindActionMap("LandMovement", throwIfNotFound: true);
        m_LandMovement_Movement = m_LandMovement.FindAction("Movement", throwIfNotFound: true);
        m_LandMovement_Interact = m_LandMovement.FindAction("Interact", throwIfNotFound: true);
        m_LandMovement_Embody = m_LandMovement.FindAction("Embody", throwIfNotFound: true);
        m_LandMovement_Jump = m_LandMovement.FindAction("Jump", throwIfNotFound: true);
        m_LandMovement_Special = m_LandMovement.FindAction("Special", throwIfNotFound: true);
        m_LandMovement_Pause = m_LandMovement.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // LandMovement
    private readonly InputActionMap m_LandMovement;
    private ILandMovementActions m_LandMovementActionsCallbackInterface;
    private readonly InputAction m_LandMovement_Movement;
    private readonly InputAction m_LandMovement_Interact;
    private readonly InputAction m_LandMovement_Embody;
    private readonly InputAction m_LandMovement_Jump;
    private readonly InputAction m_LandMovement_Special;
    private readonly InputAction m_LandMovement_Pause;
    public struct LandMovementActions
    {
        private @LandControls m_Wrapper;
        public LandMovementActions(@LandControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_LandMovement_Movement;
        public InputAction @Interact => m_Wrapper.m_LandMovement_Interact;
        public InputAction @Embody => m_Wrapper.m_LandMovement_Embody;
        public InputAction @Jump => m_Wrapper.m_LandMovement_Jump;
        public InputAction @Special => m_Wrapper.m_LandMovement_Special;
        public InputAction @Pause => m_Wrapper.m_LandMovement_Pause;
        public InputActionMap Get() { return m_Wrapper.m_LandMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LandMovementActions set) { return set.Get(); }
        public void SetCallbacks(ILandMovementActions instance)
        {
            if (m_Wrapper.m_LandMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnInteract;
                @Embody.started -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnEmbody;
                @Embody.performed -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnEmbody;
                @Embody.canceled -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnEmbody;
                @Jump.started -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnJump;
                @Special.started -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnSpecial;
                @Special.performed -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnSpecial;
                @Special.canceled -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnSpecial;
                @Pause.started -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_LandMovementActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_LandMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Embody.started += instance.OnEmbody;
                @Embody.performed += instance.OnEmbody;
                @Embody.canceled += instance.OnEmbody;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Special.started += instance.OnSpecial;
                @Special.performed += instance.OnSpecial;
                @Special.canceled += instance.OnSpecial;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public LandMovementActions @LandMovement => new LandMovementActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface ILandMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnEmbody(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSpecial(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
