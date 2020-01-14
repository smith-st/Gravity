using UnityEngine;
using System.Collections;

public static class MathUtil {

	public static float AngleBetweenTwoPoints (Vector2 p1, Vector2 p2){
		var dx = p2.x - p1.x;
		var dy = p2.y - p1.y;
		return Rad2Grad( Mathf.Atan2(dy,dx));
	}

    public static Vector2 rotateVector(Vector2 vec, float angle){
        angle = Grad2Rad (angle);
        return new Vector2 (
            vec.x * Mathf.Cos(angle) - vec.y * Mathf.Sin(angle),
            vec.x * Mathf.Sin(angle) + vec.y * Mathf.Cos(angle)
        );
    }

    private static float Rad2Grad(float angle){
        return angle * 180 / Mathf.PI;
    }

    private static float Grad2Rad(float angle){
        return angle * Mathf.PI / 180;
    }
}
