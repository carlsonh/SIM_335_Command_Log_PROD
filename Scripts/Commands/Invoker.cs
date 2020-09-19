using UnityEngine;

class Invoker
{//Knows how to execute a command & bookkeeper
    private Command command;

    public bool bIsLogging = true;
    public bool bIsLoggingToUI = true;

    public void SetCommand(Command incCommand)
    {
        command = incCommand;
    }

    public void ExecuteCommand()
    {
        if (bIsLogging)
        {//Remember the command for later
            CommandLog.commands.Enqueue(command);
        }

        if (bIsLoggingToUI)
        {//Add the command to on-screen log
            CommandLogUI.instance.UpdateCommandLogDisplay(command.GetType().Name);
        }
        command.Execute();
    }
}
