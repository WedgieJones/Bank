using BankStartWeb.Data;
using BankStartWeb.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTest.Services
{
	[TestClass]
	public class TransactionServiceTests
	{
		private ApplicationDbContext _context;
		private readonly TransactionServices _sut;

		public TransactionServiceTests()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;
			_context = new ApplicationDbContext(options);
			_sut = new TransactionServices(_context);

		}
		[TestMethod]
		public void Deposit_negative_amount_should_respond_AmountIsNegative()
		{
			var result = _sut.Deposit(2, "", -2);
			Assert.AreEqual(ITransactionServices.ErrorCode.AmountIsNegative, result);
		}
		[TestMethod]
		public void Deposit_positive_amount_should_respond_ok()
		{
			var a = new Account();
			a.Balance = 2;
			a.AccountType = "Test";
			_context.Accounts.Add(a);
			_context.SaveChanges();
			var result = _sut.Deposit(1, "", 2);
			Assert.AreEqual(ITransactionServices.ErrorCode.Ok, result);
		}
		[TestMethod]
		public void Withdraw_amount_higher_than_Balance_should_respond_BalanceTooLow()
		{
			var a = new Account();
			a.Balance = 2;
			a.AccountType = "Test";
			_context.Accounts.Add(a);
			_context.SaveChanges();
			var result = _sut.Withdraw(1, "", 6);
			Assert.AreEqual(ITransactionServices.ErrorCode.BalanceTooLow, result);
		}
		[TestMethod]
		public void Transfer_negative_amount_should_respond_AmountIsNegative()
		{
			var result = _sut.Transfer(1, 2, -2);
			Assert.AreEqual(ITransactionServices.ErrorCode.AmountIsNegative, result);
		}
		[TestMethod]
		public void Transfer_amount_higher_than_Balance_should_respond_BalanceTooLow()
		{
			var a = new Account();
			var b = new Account();
			a.Balance = 2;
			a.AccountType = "Test";
			b.Balance = 0;
			b.AccountType = "Test";
			_context.Accounts.Add(a);
			_context.Accounts.Add(b);
			_context.SaveChanges();
			var result = _sut.Transfer(1, 2, 6);
			Assert.AreEqual(ITransactionServices.ErrorCode.BalanceTooLow, result);
		}
		[TestMethod]
		public void Transfer_amount_lower_than_Balance_should_respond_Ok()
		{
			var a = new Account();
			var b = new Account();
			a.Balance = 2;
			a.AccountType = "Test";
			b.Balance = 0;
			b.AccountType = "Test";
			_context.Accounts.Add(a);
			_context.Accounts.Add(b);
			_context.SaveChanges();
			var result = _sut.Transfer(1, 2, 1);
			Assert.AreEqual(ITransactionServices.ErrorCode.Ok, result);
		}
		
		[TestMethod]
		public void Check_If_TransAction_Is_Correct_Created()
		{
			var a = new Account();
			var b = new Account();
			a.Balance = 500;
			a.AccountType = "Test";
			a.Created = DateTime.Now;
			a.Transactions = new List<Transaction>();
			b.Balance = 500;
			b.AccountType = "Test";
			b.Created = DateTime.Now;
			b.Transactions = new List<Transaction>();

			_context.Accounts.Add(a);
			_context.Accounts.Add(b);
			_context.SaveChanges();

			var result = _sut.Transfer(1, 2, 500);
			var fromTransaction = a.Transactions.Last();
			Assert.AreEqual("Credit", fromTransaction.Type);
			Assert.AreEqual("Overförning till kto:2", fromTransaction.Operation);
			Assert.AreEqual(0, fromTransaction.NewBalance);
			var toTransaction = b.Transactions.Last();
			Assert.AreEqual("Debit", toTransaction.Type);
			Assert.AreEqual("Overförning från kto:1" , toTransaction.Operation);
			Assert.AreEqual(1000, toTransaction.NewBalance);
			Assert.AreEqual(ITransactionServices.ErrorCode.Ok, result);

		}
		
	}
}
