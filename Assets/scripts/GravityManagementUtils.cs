using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GravityManagementUtils
{
    public static GravityManagement gravityManager = GravityManagement.Instance;
    public static float GetForceBetween(Rigidbody pulled, Rigidbody to, bool clamped)
    {
        float distance = (pulled.position - to.position).magnitude;
        if (clamped)
            distance = Mathf.Clamp(distance, gravityManager.MinDistanceClampBorder, distance);

        return pulled.mass * to.mass / (distance * distance);
    }

    public static float GetForceBetween(Vector3 pulledObjPos, float pulledObjMass, Vector3 magneticObjPos, float magneticObjMass, bool clamped)
    {
        float distance = (pulledObjPos - magneticObjPos).magnitude;
        if (clamped)
            distance = Mathf.Clamp(distance, gravityManager.MinDistanceClampBorder, distance);

        return pulledObjMass * magneticObjMass / (distance * distance);
    }
    
    public static float GetVelocityFromForce(float force, float affectedObjectMass, ForceMode forceMode)
    {
        switch (forceMode)
        {
            case ForceMode.Force:
                return force * Time.fixedDeltaTime / affectedObjectMass;    // Точно
            case ForceMode.Acceleration:
                return force * Time.fixedDeltaTime;                         // Не точно
            case ForceMode.VelocityChange:
                return force;                                               // Не точно
            default:
                return force / affectedObjectMass;                          // Импульс (не точно)
        }
    }

    public static Vector3 GetVelocityFromForce(Vector3 force, float affectedObjectMass, ForceMode forceMode)
    {
        switch (forceMode)
        {
            case ForceMode.Force:
                return force * Time.fixedDeltaTime / affectedObjectMass;    // Точно
            case ForceMode.Acceleration:
                return force * Time.fixedDeltaTime;                         // Не точно
            case ForceMode.VelocityChange:
                return force;                                               // Не точно
            default:
                return force / affectedObjectMass;                          // Импульс (не точно)
        }
    }

    public static bool ObjectsCanGravitate(Vector3 first, Vector3 second)
    {
        float distance = Vector3.Distance(first, second);
        return distance > gravityManager.MinDistanceToMagnetize;
    }

    public static Vector3 GetOrbitalVelocity(Rigidbody rotateAroundGameObject, Rigidbody rotatingObj)
    {
        Vector3 perpendicular = (rotateAroundGameObject.position - rotatingObj.position).normalized;
        perpendicular = new Vector3(-perpendicular.y, perpendicular.x);
        perpendicular = Random.Range(0, 2) == 1 ? perpendicular : -perpendicular;
        float force = GravityManagementUtils.GetForceBetween(rotatingObj, rotateAroundGameObject, true);
        float distance = (rotatingObj.position - rotateAroundGameObject.position).magnitude;
        float velocity = Mathf.Sqrt(force * distance / rotatingObj.mass);
        return perpendicular * velocity;
    }

}
