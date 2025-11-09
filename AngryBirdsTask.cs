using System;

namespace AngryBirds;

public static class AngryBirdsTask
{
	public static double AccelerationOfGravity = 9.8;

	public static double FindSightAngle(double speed, double distance)
	{
		var potEnergy = distance * AccelerationOfGravity;
		var angle = Math.Asin(potEnergy / (speed * speed)) / 2;
		return angle;
	}
}