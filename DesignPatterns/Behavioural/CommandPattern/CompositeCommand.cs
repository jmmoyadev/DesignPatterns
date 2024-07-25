using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioural.CommandPattern.CompositeCommand;

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

    bool Success { get; }
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
    public bool Success { get; private set; }

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
                Success = account.Deposit(amount);
                break;

            case Action.Withdraw:
                Success = account.Withdraw(amount);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Undo()
    {
        if (!Success)
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

public class CompositeBankAccountCommand : List<BankAccountCommand>, ICommand
{
    public bool Success => this.TrueForAll(cmd => cmd.Success);

    public virtual void Call()
    {
        ForEach(cmd => cmd.Call());
    }

    public virtual void Undo()
    {
        foreach (var cmd in ((IEnumerable<BankAccountCommand>)this).Reverse())
        {
            cmd.Undo();
        }
    }
}

public class MoneyTransferCommand : CompositeBankAccountCommand
{
    public MoneyTransferCommand(BankAccount from, BankAccount to, int amount)
    {
        AddRange(new[]
        {
            new BankAccountCommand(from, BankAccountCommand.Action.Withdraw, amount),
            new BankAccountCommand(to, BankAccountCommand.Action.Deposit, amount)
        });
    }

    public override void Call()
    {
        BankAccountCommand last = null;
        foreach (var cmd in this)
        {
            if (last == null || last.Success)
            {
                cmd.Call();
                last = cmd;
            }
            else
            {
                cmd.Undo();
                break;
            }
        }
    }
}

public class Demo
{
    public static void Main(string[] args)
    {
        var from = new BankAccount();
        from.Deposit(100);

        var to = new BankAccount();

        var mtc = new MoneyTransferCommand(from, to, 100);
        mtc.Call();

        Console.WriteLine("account 1: " + from);
        Console.WriteLine("account 2: " + to);

        mtc.Undo();
        Console.WriteLine("account 1: " + from);
        Console.WriteLine("account 2: " + to);
    }
}