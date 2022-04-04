using System.Collections;
using System.Collections.Generic;
using System;

public enum SimulationStatus {Inactive, Active, Completed}

public class SimulationStatusEventArgs : EventArgs 
{
    public SimulationStatus Status {get; set;}
}
public class SimulationStatusHandler
{
    private SimulationStatus value = SimulationStatus.Inactive;
    public event EventHandler<SimulationStatusEventArgs> Changed;
    public event EventHandler<SimulationStatusEventArgs> Inactivated;
    public event EventHandler<SimulationStatusEventArgs> Activated;
    public event EventHandler<SimulationStatusEventArgs> Completed;
    public SimulationStatus Value {
        get {return this.value; }
        set {
            if(this.value != value) {
                this.value = value;
                if(Changed != null) {
                    Changed(
                        this, 
                        new SimulationStatusEventArgs(){Status = this.value});
                }
                if(this.value == SimulationStatus.Inactive) {
                    if(Inactivated != null) {
                        Inactivated(
                            this, 
                            new SimulationStatusEventArgs(){Status = this.value});
                    }
                }
                else if (this.value == SimulationStatus.Active) {
                    if(Activated != null) {
                        Activated(
                            this, 
                            new SimulationStatusEventArgs(){Status = this.value});
                    }
                }
                else if (this.value == SimulationStatus.Completed) {
                    if(Completed != null) {
                        Completed(
                            this, 
                            new SimulationStatusEventArgs(){Status = this.value});
                    }
                }
            }
        }
    }

    public SimulationStatusHandler() {
        this.value = SimulationStatus.Inactive;
        this.Changed = null;
    }
}
