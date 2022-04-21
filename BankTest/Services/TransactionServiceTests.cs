﻿using BankStartWeb.Data;
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
		public void Deposit_negativ_amount_should_respond_AmountIsNegativ()
		{
			var result = _sut.Deposit(2, "", -2);
			Assert.AreEqual(ITransactionServices.ErrorCode.AmountIsNegativ, result);
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

	}
}
