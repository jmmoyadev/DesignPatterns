using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioural.CommandPattern.CommandPatternExample;

public class BankAccount
{
    private int balance;
    private int overdraftLimit = -500;

    public bool Deposit(int amount)
    {
        balance += amount;
        Console.WriteLine($"Deposited ${amount}, balance is now {balance}");
        return true;
    }

    public bool Withdraw(int amount)
    {
        if (balance - amount >= overdraftLimit)
        {
            balance -= amount;
            Console.WriteLine($"Withdrew ${amount}, balance is now {balance}");
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"{nameof(balance)}: {balance}";
    }
}

public interface ICommand
{
    void Call();

    void Undo();
}

public class BankAccountCommand : ICommand
{
    public enum Action
    {
        Deposit, Withdraw
    }

    private BankAccount account;
    private readonly Action action;
    private int amount;
    private bool succeeded;

    public BankAccountCommand(BankAccount account, Action action, int amount)
    {
        this.account = account ?? throw new ArgumentNullException(nameof(account));
        this.action = action;
        this.amount = amount;
    }

    public void Call()
    {
        switch (action)
        {
            case Action.Deposit:
                succeeded = account.Deposit(amount);
                break;

            case Action.Withdraw:
                succeeded = account.Withdraw(amount);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Undo()
    {
        if (!succeeded)
            return;

        switch (action)
        {
            case Action.Deposit:
                account.Withdraw(amount);
                break;

            case Action.Withdraw:
                account.Deposit(amount);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public class Demo
{
    public static void Main(string[] args)
    {
        var ba = new BankAccount();
        var commands = new List<BankAccountCommand>
        {
            new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100),
            new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 1000)
        };

        Console.WriteLine(ba);

        foreach (var command in commands)
        {
            command.Call();
        }

        Console.WriteLine(ba);

        foreach (var command in Enumerable.Reverse(commands))
        {
            command.Undo();
        }

        Console.WriteLine(ba);
    }
}