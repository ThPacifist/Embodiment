// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Control/Water/WaterControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @WaterControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @WaterControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""WaterControls"",
    ""maps"": [
        {
            ""name"": ""WaterMovement"",
            ""id"": ""643d04c0-3292-4935-95fc-010548de2311"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""2c8c0f9a-72e4-428f-b08d-0b6c3b5d332d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""d379295b-7a5c-4919-a40f-a5e2f03e1b82"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Embody"",
                    ""type"": ""Button"",
                    ""id"": ""01ee653c-2527-4ff1-b2af-e2cf0b11f965"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""5c309760-7ef0-487b-9047-5cadab2bfdd3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Special"",
                    ""type"": ""Button"",
                    ""id"": ""a678015e-b8b5-4560-adec-9f4d0ef4710e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""29567a62-6f71-4537-8dd0-98078966ea12"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""59b747ad-db6e-495e-95e2-83313daeb9f4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1cf72be2-f45e-4ada-9547-09a7b5421a27"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""02cb536d-dfdc-41d9-8bbb-fdf83a10f876"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a4c641f6-d664-4383-b35e-b61d392081a0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""62e01f21-9556-472f-b478-7768e98349dd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""84bf8e87-2ab6-47dc-b453-6ae41f37b08a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""815102b5-9d4a-4a2f-a3d7-1b3132c55d59"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""279cdf8d-ad17-47e1-b4f4-0ee5bef3aa41"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8e68e6a5-e238-43ac-a682-075f1bb3b58e"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0356b640-dd8f-49a6-aee3-e626ffe07c14"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""66073cbe-d5cb-463e-88be-b66bd2b6b246"",
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
                    ""id"": ""49afba52-b2ba-4831-9e2d-3837bf5b16bd"",
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
                    ""id"": ""d983624e-5aed-4d2e-b977-610091cfcf5e"",
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
                    ""id"": ""abbd65e1-4d85-4493-a234-10d4cca67c51"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Embody"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f80da7e-24f0-4eef-ad65-a850f1234e4d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""507d679f-93e9-40dd-a615-8071d7d42080"",
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
                    ""id"": ""cd08e9ce-0267-4016-9289-11ee9b16e900"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4fe8d33-a611-46bf-bd86-2e466f8b3bdc"",
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
                    ""id"": ""1904767a-d9b5-40d5-b11a-b3cb38f279f1"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c213e103-5a2b-43bb-8a8d-5bcdb722f9af"",
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
        // WaterMovement
        m_WaterMovement = asset.FindActionMap("WaterMovement", throwIfNotFound: true);
        m_WaterMovement_Movement = m_WaterMovement.FindAction("Movement", throwIfNotFound: true);
        m_WaterMovement_Interact = m_WaterMovement.FindAction("Interact", throwIfNotFound: true);
        m_WaterMovement_Embody = m_WaterMovement.FindAction("Embody", throwIfNotFound: true);
        m_WaterMovement_Jump = m_WaterMovement.FindAction("Jump", throwIfNotFound: true);
        m_WaterMovement_Special = m_WaterMovement.FindAction("Special", throwIfNotFound: true);
        m_WaterMovement_Pause = m_WaterMovement.FindAction("Pause", throwIfNotFound: true);
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

    // WaterMovement
    private readonly InputActionMap m_WaterMovement;
    private IWaterMovementActions m_WaterMovementActionsCallbackInterface;
    private readonly InputAction m_WaterMovement_Movement;
    private readonly InputAction m_WaterMovement_Interact;
    private readonly InputAction m_WaterMovement_Embody;
    private readonly InputAction m_WaterMovement_Jump;
    private readonly InputAction m_WaterMovement_Special;
    private readonly InputAction m_WaterMovement_Pause;
    public struct WaterMovementActions
    {
        private @WaterControls m_Wrapper;
        public WaterMovementActions(@WaterControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_WaterMovement_Movement;
        public InputAction @Interact => m_Wrapper.m_WaterMovement_Interact;
        public InputAction @Embody => m_Wrapper.m_WaterMovement_Embody;
        public InputAction @Jump => m_Wrapper.m_WaterMovement_Jump;
        public InputAction @Special => m_Wrapper.m_WaterMovement_Special;
        public InputAction @Pause => m_Wrapper.m_WaterMovement_Pause;
        public InputActionMap Get() { return m_Wrapper.m_WaterMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WaterMovementActions set) { return set.Get(); }
        public void SetCallbacks(IWaterMovementActions instance)
        {
            if (m_Wrapper.m_WaterMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnInteract;
                @Embody.started -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnEmbody;
                @Embody.performed -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnEmbody;
                @Embody.canceled -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnEmbody;
                @Jump.started -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnJump;
                @Special.started -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnSpecial;
                @Special.performed -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnSpecial;
                @Special.canceled -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnSpecial;
                @Pause.started -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_WaterMovementActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_WaterMovementActionsCallbackInterface = instance;
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
    public WaterMovementActions @WaterMovement => new WaterMovementActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IWaterMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnEmbody(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSpecial(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
