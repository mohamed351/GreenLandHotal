using Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityDataLayer;
namespace DesktopProjectGreenLandHotal.Reservation
{
    public enum TypeOfForm
    {
        Comming, 
        Leaving
    }
    public partial class SetCommingDate : Form
    {
        private readonly IReservationRepository reservationRepository;
        private EntityDataLayer.Reservation reservation;
        public ReservationInfoView UserInfo { get; set; }
        public TypeOfForm TypeOfForm { get; set; }
        public SetCommingDate(IReservationRepository reservationRepository)
        {
            InitializeComponent();
         
            this.reservationRepository = reservationRepository;
        }

        private void DetectOfInitalForm()
        {
            switch (TypeOfForm)
            {
                case TypeOfForm.Comming:
                    btnSubmitComming.Text = "SubmitComming time";
                    this.Text = "Comming Date";
                    break;
                case TypeOfForm.Leaving:
                    btnSubmitComming.Text = "SubmitLeaving comming";
                    this.Text = "Leaving Date";
                    break;
                default:
                    break;
            }
        }

        private void btnSubmitComming_Click(object sender, EventArgs e)
        {
            switch (TypeOfForm)
            {
                case TypeOfForm.Comming:
                    SetCommingTime();
                    break;
                case TypeOfForm.Leaving:
                    SetLeavingDateTime();
                    break;
                default:
                    break;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            textBox12.Text = DateTime.Now.ToLongDateString();
        }

        private void SetCommingDate_Load(object sender, EventArgs e)
        {
            DetectOfInitalForm();
            if (UserInfo == null)
            {
                MessageBox.Show("Please Select Reservation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                this.reservation = reservationRepository.GetIQueryable().FirstOrDefault(a => a.UserID == UserInfo.UserID
                && a.ReservationDate == UserInfo.ReservationDate && a.RoomID ==UserInfo.RoomID);
                textBox1.Text = $"UserID  :{UserInfo.UserID}";
                textBox2.Text = $"Name : {UserInfo.Name}";
                textBox3.Text = $"UserName : {UserInfo.UserName}";
                textBox4.Text = $"Capacity : {UserInfo.NumberOfPeople}";
                textBox5.Text = $"Beds : {UserInfo.NumberOfBeds}";
                textBox6.Text = $"ReservationDate  :{UserInfo.ReservationDate}";
                textBox6.Text = $"DepatureDate : {UserInfo.DepatureDate}";
                textBox7.Text = $"RoomNumber : {UserInfo.Number}";
                textBox8.Text = $"Approved : {UserInfo.IsApproved}";
                textBox9.Text = $"IsDeleted : {UserInfo.IsDeleted}";
                textBox10.Text = $"Comming Date: {UserInfo.ComingDate}";
                textBox11.Text = $"Leave Date {UserInfo.LeavingDate}";
                textBox12.Text = DateTime.Now.ToLongDateString();
                WindowsFormsUtilities.Helpers.DisableAllControls(this.groupBox1);
                Timer timer = new Timer();
                timer.Interval = 1000;
                timer.Enabled = true;
                timer.Tick += Timer_Tick;
                
            }
        }

        private void SetCommingTime()
        {
            if (reservation.IsApproved && reservation.IsDeleted == false)
            {

                reservation.ComingDate = DateTime.Now;
                this.reservationRepository.Edit(reservation);
                if(this.reservationRepository.SaveChanges() >0)
                {
                    this.reservationRepository.EditAttach(reservation);
                    MessageBox.Show("This resvation has comming Date", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("This resvation didn't Approved or Cancelled","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void SetLeavingDateTime()
        {
            if (reservation.IsApproved && reservation.IsDeleted == false && reservation.ComingDate != null)
            {
                reservation.Checkout = true;
                reservation.LeavingDate = DateTime.Now;
                this.reservationRepository.Edit(reservation);
                if (this.reservationRepository.SaveChanges() > 0)
                {
                    this.reservationRepository.EditAttach(reservation);
                    MessageBox.Show("Successfull Checkout", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                MessageBox.Show("This resvation didn't Approved or hasn't comming Date or Cancelled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
