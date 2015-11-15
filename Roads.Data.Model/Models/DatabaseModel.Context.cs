﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Roads.DataBase.Model.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DatabaseModelContainer : DbContext
    {
        public DatabaseModelContainer()
            : base("name=DatabaseModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<RouteNode> RouteNodes { get; set; }
        public virtual DbSet<CityNode> CityNodes { get; set; }
        public virtual DbSet<RegionNode> RegionNodes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<FeedbackItem> FeedbackItems { get; set; }
        public virtual DbSet<FeedbackModel> FeedbackModels { get; set; }
        public virtual DbSet<FeedbackValue> FeedbackValues { get; set; }
        public virtual DbSet<Trek> Treks { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
    }
}
