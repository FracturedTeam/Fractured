using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.HashSetUtil;
using Object = UnityEngine.Object;


#pragma warning disable 0618 // Disabled warning due to SetVertices being deprecated until new release with SetMesh() is available.

namespace TMPro.Examples
{

    public class TMP_TextSelector_B : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public RectTransform TextPopup_Prefab_01;

        private RectTransform m_TextPopup_RectTransform;
        private TextMeshProUGUI m_TextPopup_TMPComponent;
        private const string k_LinkText = "You have selected link <#ffff00>";
        private const string k_WordText = "Word Index: <#ffff00>";
        
        private ObservableHashSet<Glass> shardsOnTop = new();


        private TextMeshProUGUI m_TextMeshPro;
        private Canvas m_Canvas;

        // Flags
        private bool isHoveringObject;
        private int m_selectedWord = -1;
        private int m_selectedLink = -1;
        private int m_lastIndex = -1;

        private Matrix4x4 m_matrix;

        private TMP_MeshInfo[] m_cachedMeshInfoVertexData;

        void Awake()
        {
            m_TextMeshPro = gameObject.GetComponent<TextMeshProUGUI>();


            m_Canvas = gameObject.GetComponentInParent<Canvas>();

            // Get a reference to the camera if Canvas Render Mode is not ScreenSpace Overlay.

            // Create pop-up text object which is used to show the link information.
            m_TextPopup_RectTransform = Instantiate(TextPopup_Prefab_01) as RectTransform;
            m_TextPopup_RectTransform.SetParent(m_Canvas.transform, false);
            m_TextPopup_TMPComponent = m_TextPopup_RectTransform.GetComponentInChildren<TextMeshProUGUI>();
            m_TextPopup_RectTransform.gameObject.SetActive(false);
        }

        private void Start()
        {
            shardsOnTop = new ObservableHashSet<Glass>();
        }

        void OnEnable()
        {
            // Subscribe to event fired when text object has been regenerated.
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
        }

        void OnDisable()
        {
            // UnSubscribe to event fired when text object has been regenerated.
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
        }


        void ON_TEXT_CHANGED(Object obj)
        {
            if (obj == m_TextMeshPro)
            {
                // Update cached vertex data.
                m_cachedMeshInfoVertexData = m_TextMeshPro.textInfo.CopyMeshInfoVertexData();
            }
        }
        
        internal void OnInteract(bool isUnder, Glass shard) {
            if(!shard)
                return;
            if (isUnder) 
                shardsOnTop.Add(shard);
            else if(shardsOnTop.Contains(shard))
                shardsOnTop.Remove(shard);
        }


        void LateUpdate()
        {
            if (isHoveringObject)
            {
                // Check if Mouse Intersects any of the characters. If so, assign a random color.
                #region Handle Character Selection
                int charIndex = TMP_TextUtilities.FindIntersectingCharacter(m_TextMeshPro, Input.mousePosition, PlayerController.Instance.cinemachineBrain.OutputCamera, true);

                // Undo Swap and Vertex Attribute changes.
                if (charIndex == -1 || charIndex != m_lastIndex)
                {
                    RestoreCachedVertexAttributes(m_lastIndex);
                    m_lastIndex = -1;
                }

                if (charIndex != -1 && charIndex != m_lastIndex && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
                {
                    m_lastIndex = charIndex;

                    // Get the index of the material / sub text object used by this character.
                    int materialIndex = m_TextMeshPro.textInfo.characterInfo[charIndex].materialReferenceIndex;

                    // Get the index of the first vertex of the selected character.
                    int vertexIndex = m_TextMeshPro.textInfo.characterInfo[charIndex].vertexIndex;

                    // Get a reference to the vertices array.
                    Vector3[] vertices = m_TextMeshPro.textInfo.meshInfo[materialIndex].vertices;

                    // Determine the center point of the character.
                    Vector2 charMidBasline = (vertices[vertexIndex + 0] + vertices[vertexIndex + 2]) / 2;

                    // Need to translate all 4 vertices of the character to aligned with middle of character / baseline.
                    // This is needed so the matrix TRS is applied at the origin for each character.
                    Vector3 offset = charMidBasline;

                    // Translate the character to the middle baseline.
                    vertices[vertexIndex + 0] = vertices[vertexIndex + 0] - offset;
                    vertices[vertexIndex + 1] = vertices[vertexIndex + 1] - offset;
                    vertices[vertexIndex + 2] = vertices[vertexIndex + 2] - offset;
                    vertices[vertexIndex + 3] = vertices[vertexIndex + 3] - offset;

                    float zoomFactor = 1.5f;

                    // Setup the Matrix for the scale change.
                    m_matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * zoomFactor);

                    // Apply Matrix operation on the given character.
                    vertices[vertexIndex + 0] = m_matrix.MultiplyPoint3x4(vertices[vertexIndex + 0]);
                    vertices[vertexIndex + 1] = m_matrix.MultiplyPoint3x4(vertices[vertexIndex + 1]);
                    vertices[vertexIndex + 2] = m_matrix.MultiplyPoint3x4(vertices[vertexIndex + 2]);
                    vertices[vertexIndex + 3] = m_matrix.MultiplyPoint3x4(vertices[vertexIndex + 3]);

                    // Translate the character back to its original position.
                    vertices[vertexIndex + 0] = vertices[vertexIndex + 0] + offset;
                    vertices[vertexIndex + 1] = vertices[vertexIndex + 1] + offset;
                    vertices[vertexIndex + 2] = vertices[vertexIndex + 2] + offset;
                    vertices[vertexIndex + 3] = vertices[vertexIndex + 3] + offset;

                    // Change Vertex Colors of the highlighted character
                    Color32 c = new Color32(255, 255, 192, 0);

                    // Get a reference to the vertex color
                    Color32[] vertexColors = m_TextMeshPro.textInfo.meshInfo[materialIndex].colors32;

                    vertexColors[vertexIndex + 0] = c;
                    vertexColors[vertexIndex + 1] = c;
                    vertexColors[vertexIndex + 2] = c;
                    vertexColors[vertexIndex + 3] = c;


                    // Get a reference to the meshInfo of the selected character.
                    TMP_MeshInfo meshInfo = m_TextMeshPro.textInfo.meshInfo[materialIndex];

                    // Get the index of the last character's vertex attributes.
                    int lastVertexIndex = vertices.Length - 4;

                    // Swap the current character's vertex attributes with those of the last element in the vertex attribute arrays.
                    // We do this to make sure this character is rendered last and over other characters.
                    meshInfo.SwapVertexData(vertexIndex, lastVertexIndex);

                    // Need to update the appropriate
                    m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
                }
                #endregion


                #region Word Selection Handling
                //Check if Mouse intersects any words and if so assign a random color to that word.
                int wordIndex = TMP_TextUtilities.FindIntersectingWord(m_TextMeshPro, Input.mousePosition, PlayerController.Instance.cinemachineBrain.OutputCamera);

                // Clear previous word selection.
                if (m_TextPopup_RectTransform != null && m_selectedWord != -1 && (wordIndex == -1 || wordIndex != m_selectedWord))
                {
                    TMP_WordInfo wInfo = m_TextMeshPro.textInfo.wordInfo[m_selectedWord];

                    // Iterate through each of the characters of the word.
                    for (int i = 0; i < wInfo.characterCount; i++)
                    {
                        int characterIndex = wInfo.firstCharacterIndex + i;

                        // Get the index of the material / sub text object used by this character.
                        int meshIndex = m_TextMeshPro.textInfo.characterInfo[characterIndex].materialReferenceIndex;

                        // Get the index of the first vertex of this character.
                        int vertexIndex = m_TextMeshPro.textInfo.characterInfo[characterIndex].vertexIndex;

                        // Get a reference to the vertex color
                        Color32[] vertexColors = m_TextMeshPro.textInfo.meshInfo[meshIndex].colors32;

                        Color32 c = new Color32(0,0,0,255);

                        vertexColors[vertexIndex + 0] = c;
                        vertexColors[vertexIndex + 1] = c;
                        vertexColors[vertexIndex + 2] = c;
                        vertexColors[vertexIndex + 3] = c;
                    }

                    // Update Geometry
                    m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

                    m_selectedWord = -1;
                }


                // Word Selection Handling
                if (wordIndex != -1 && wordIndex != m_selectedWord)
                {
                    m_selectedWord = wordIndex;

                    TMP_WordInfo wInfo = m_TextMeshPro.textInfo.wordInfo[wordIndex];

                    // Iterate through each of the characters of the word.
                    for (int i = 0; i < wInfo.characterCount; i++)
                    {
                        int characterIndex = wInfo.firstCharacterIndex + i;

                        // Get the index of the material / sub text object used by this character.
                        int meshIndex = m_TextMeshPro.textInfo.characterInfo[characterIndex].materialReferenceIndex;

                        int vertexIndex = m_TextMeshPro.textInfo.characterInfo[characterIndex].vertexIndex;

                        // Get a reference to the vertex color
                        Color32[] vertexColors = m_TextMeshPro.textInfo.meshInfo[meshIndex].colors32;

                        Color32 c = vertexColors[vertexIndex + 0].Tint(00f);

                        vertexColors[vertexIndex + 0] = c;
                        vertexColors[vertexIndex + 1] = c;
                        vertexColors[vertexIndex + 2] = c;
                        vertexColors[vertexIndex + 3] = c;
                    }

                    // Update Geometry
                    m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

                }
                #endregion
            }
            else
            {
                // Restore any character that may have been modified
                if (m_lastIndex != -1)
                {
                    RestoreCachedVertexAttributes(m_lastIndex);
                    m_lastIndex = -1;
                }
            }

        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log("OnPointerEnter()");
            isHoveringObject = true;
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            //Debug.Log("OnPointerExit()");
            isHoveringObject = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //Debug.Log("OnPointerUp()");
        }


        void RestoreCachedVertexAttributes(int index)
        {
            if (index == -1 || index > m_TextMeshPro.textInfo.characterCount - 1) return;

            // Get the index of the material / sub text object used by this character.
            int materialIndex = m_TextMeshPro.textInfo.characterInfo[index].materialReferenceIndex;

            // Get the index of the first vertex of the selected character.
            int vertexIndex = m_TextMeshPro.textInfo.characterInfo[index].vertexIndex;

            // Restore Vertices
            // Get a reference to the cached / original vertices.
            Vector3[] src_vertices = m_cachedMeshInfoVertexData[materialIndex].vertices;

            // Get a reference to the vertices that we need to replace.
            Vector3[] dst_vertices = m_TextMeshPro.textInfo.meshInfo[materialIndex].vertices;

            // Restore / Copy vertices from source to destination
            dst_vertices[vertexIndex + 0] = src_vertices[vertexIndex + 0];
            dst_vertices[vertexIndex + 1] = src_vertices[vertexIndex + 1];
            dst_vertices[vertexIndex + 2] = src_vertices[vertexIndex + 2];
            dst_vertices[vertexIndex + 3] = src_vertices[vertexIndex + 3];

            // Restore Vertex Colors
            // Get a reference to the vertex colors we need to replace.
            Color32[] dst_colors = m_TextMeshPro.textInfo.meshInfo[materialIndex].colors32;

            // Get a reference to the cached / original vertex colors.
            Color32[] src_colors = m_cachedMeshInfoVertexData[materialIndex].colors32;

            // Copy the vertex colors from source to destination.
            dst_colors[vertexIndex + 0] = src_colors[vertexIndex + 0];
            dst_colors[vertexIndex + 1] = src_colors[vertexIndex + 1];
            dst_colors[vertexIndex + 2] = src_colors[vertexIndex + 2];
            dst_colors[vertexIndex + 3] = src_colors[vertexIndex + 3];

            // Restore UV0S
            // UVS0
            Vector4[] src_uv0s = m_cachedMeshInfoVertexData[materialIndex].uvs0;
            Vector4[] dst_uv0s = m_TextMeshPro.textInfo.meshInfo[materialIndex].uvs0;
            dst_uv0s[vertexIndex + 0] = src_uv0s[vertexIndex + 0];
            dst_uv0s[vertexIndex + 1] = src_uv0s[vertexIndex + 1];
            dst_uv0s[vertexIndex + 2] = src_uv0s[vertexIndex + 2];
            dst_uv0s[vertexIndex + 3] = src_uv0s[vertexIndex + 3];

            // UVS2
            Vector2[] src_uv2s = m_cachedMeshInfoVertexData[materialIndex].uvs2;
            Vector2[] dst_uv2s = m_TextMeshPro.textInfo.meshInfo[materialIndex].uvs2;
            dst_uv2s[vertexIndex + 0] = src_uv2s[vertexIndex + 0];
            dst_uv2s[vertexIndex + 1] = src_uv2s[vertexIndex + 1];
            dst_uv2s[vertexIndex + 2] = src_uv2s[vertexIndex + 2];
            dst_uv2s[vertexIndex + 3] = src_uv2s[vertexIndex + 3];


            // Restore last vertex attribute as we swapped it as well
            int lastIndex = (src_vertices.Length / 4 - 1) * 4;

            // Vertices
            dst_vertices[lastIndex + 0] = src_vertices[lastIndex + 0];
            dst_vertices[lastIndex + 1] = src_vertices[lastIndex + 1];
            dst_vertices[lastIndex + 2] = src_vertices[lastIndex + 2];
            dst_vertices[lastIndex + 3] = src_vertices[lastIndex + 3];

            // Vertex Colors
            src_colors = m_cachedMeshInfoVertexData[materialIndex].colors32;
            dst_colors = m_TextMeshPro.textInfo.meshInfo[materialIndex].colors32;
            dst_colors[lastIndex + 0] = src_colors[lastIndex + 0];
            dst_colors[lastIndex + 1] = src_colors[lastIndex + 1];
            dst_colors[lastIndex + 2] = src_colors[lastIndex + 2];
            dst_colors[lastIndex + 3] = src_colors[lastIndex + 3];

            // UVS0
            src_uv0s = m_cachedMeshInfoVertexData[materialIndex].uvs0;
            dst_uv0s = m_TextMeshPro.textInfo.meshInfo[materialIndex].uvs0;
            dst_uv0s[lastIndex + 0] = src_uv0s[lastIndex + 0];
            dst_uv0s[lastIndex + 1] = src_uv0s[lastIndex + 1];
            dst_uv0s[lastIndex + 2] = src_uv0s[lastIndex + 2];
            dst_uv0s[lastIndex + 3] = src_uv0s[lastIndex + 3];

            // UVS2
            src_uv2s = m_cachedMeshInfoVertexData[materialIndex].uvs2;
            dst_uv2s = m_TextMeshPro.textInfo.meshInfo[materialIndex].uvs2;
            dst_uv2s[lastIndex + 0] = src_uv2s[lastIndex + 0];
            dst_uv2s[lastIndex + 1] = src_uv2s[lastIndex + 1];
            dst_uv2s[lastIndex + 2] = src_uv2s[lastIndex + 2];
            dst_uv2s[lastIndex + 3] = src_uv2s[lastIndex + 3];

            // Need to update the appropriate
            m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        }
    }
}
