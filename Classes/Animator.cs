using System;
using System.Threading;

namespace CamTimer {
	class Animator {
		private int m_totalDurationMsec = 1000;
		private int m_fps = 33;
		private Thread m_animationThread = null;

		internal bool IsAnimating {
			get {
				return (m_animationThread != null);
			}
		}

		internal int TotalDuration {
			get {
				return m_totalDurationMsec;
			}
			set {
				if (value > 0) {
					m_totalDurationMsec = value;
				}
			}
		}

		internal int FPS {
			get {
				return m_fps;
			}
			set {
				if (m_fps > 0) {
					m_fps = value;
				} 
			}
		}

		internal void Run(EventHandler<AnimationEventArgs> callback) {
			if (m_animationThread == null) {
				m_animationThread = new Thread((ThreadStart)delegate() {
					try {
						int sleepMSec = (int)(1000f / m_fps);
						int tickStart = Environment.TickCount;
						while (true) {
							double percentCompleted = (double)(Environment.TickCount-tickStart)/m_totalDurationMsec;
							callback(this, new AnimationEventArgs(percentCompleted));
							if (percentCompleted < 1) {
								Thread.Sleep(sleepMSec);
							} else {
								break;
							}
						}
					} catch (Exception) { }
					m_animationThread = null;
				});
				m_animationThread.Start();
			}
		}

		internal void Abort() {
			try {
				if (m_animationThread != null) {
					m_animationThread.Abort();
				}
			} catch (Exception) { }
		}
	}

	class AnimationEventArgs : EventArgs {
		private double m_percentComplete;
		
		internal AnimationEventArgs(double percentComplete) {
			if (percentComplete < 0) percentComplete = 0;
			if (percentComplete > 1) percentComplete = 1;
			m_percentComplete = percentComplete;
		}

		internal double PercentComplete {
			get {
				return m_percentComplete;
			}
		}

		internal double PercentRemaining {
			get {
				return 1.0-m_percentComplete;
			}
		}

		internal bool IsLastCall {
			get {
				return m_percentComplete == 1;
			}
		}
	}
}
