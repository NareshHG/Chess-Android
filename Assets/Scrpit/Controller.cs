using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Game {
public class Controller : MonoBehaviour
{
		public PieceTheme pieceTheme;
		public BoardTheme boardTheme;
		

		MeshRenderer[, ] squareRenderers;
		SpriteRenderer[, ] squarePieceRenderers;
		
		const float pieceDepth = -0.1f;
		const float pieceDragDepth = -0.2f;

		void Awake () {
			
			CreateBoardUI ();

		}

	

	

		void CreateBoardUI () {

			Shader squareShader = Shader.Find ("Unlit/Color");
			squareRenderers = new MeshRenderer[8, 8];
			squarePieceRenderers = new SpriteRenderer[8, 8];

			for (int rank = 0; rank < 8; rank++) {
				for (int file = 0; file < 8; file++) {
					
					Transform square = GameObject.CreatePrimitive (PrimitiveType.Quad).transform;
					square.parent = transform;
					square.name = BoardRepresentation.SquareNameFromCoordinate (file, rank);
					square.position = PositionFromCoord (file, rank, 0);
					Material squareMaterial = new Material (squareShader);

					squareRenderers[file, rank] = square.gameObject.GetComponent<MeshRenderer> ();
					squareRenderers[file, rank].material = squareMaterial;

					
					SpriteRenderer pieceRenderer = new GameObject ("Piece").AddComponent<SpriteRenderer> ();
					pieceRenderer.transform.parent = square;
					pieceRenderer.transform.position = PositionFromCoord (file, rank, pieceDepth);
					pieceRenderer.transform.localScale = Vector3.one * 100 / (2000 / 6f);
					squarePieceRenderers[file, rank] = pieceRenderer;
				}
			}

			ResetSquareColours ();
		}

		void ResetSquarePositions () {
			for (int rank = 0; rank < 8; rank++) {
				for (int file = 0; file < 8; file++) {
					if (file == 0 && rank == 0) {
						
					}
					
					squareRenderers[file, rank].transform.position = PositionFromCoord (file, rank, 0);
					squarePieceRenderers[file, rank].transform.position = PositionFromCoord (file, rank, pieceDepth);
				}
			}

			
		}

		
		

		public void ResetSquareColours (bool highlight = true) {
			for (int rank = 0; rank < 8; rank++) {
				for (int file = 0; file < 8; file++) {
					SetSquareColour (new Coord (file, rank), boardTheme.lightSquares.normal, boardTheme.darkSquares.normal);
				}
			}
		
		}

		void SetSquareColour (Coord square, Color lightCol, Color darkCol) {
			squareRenderers[square.fileIndex, square.rankIndex].material.color = (square.IsLightSquare ()) ? lightCol : darkCol;
		}

		public Vector3 PositionFromCoord (int file, int rank, float depth = 0) {
			
				return new Vector3 (-3.5f + file, -3.5f + rank, depth);
		}

		public Vector3 PositionFromCoord (Coord coord, float depth = 0) {
			return PositionFromCoord (coord.fileIndex, coord.rankIndex, depth);
		}

	}
}