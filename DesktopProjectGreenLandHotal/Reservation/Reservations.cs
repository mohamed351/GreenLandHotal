using Repositories;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using EntityDataLayer;

namespace DesktopProjectGreenLandHotal.Reservation
{
    public partial class Reservations : Form
    {
        IReservationRepository reservationRepository;
        public Reservations()
        {
            InitializeComponent();
            reservationRepository = new ReservationRepository(new GreenLandProjectEntities());
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
         
        }

        private void Reservations_Load(object sender, EventArgs e)
        {
            SelectData();

        }

        private void SelectData()
        {
            var resvationData = reservationRepository
                .GetAllWithTracking()
                .OrderByDescending(a => a.ReservationDate)
                .Select(a =>
                new ReservationInfoView
                 {
                    UserID = a.UserID,
                    RoomID = a.RoomID,
                    UserName = a.AspNetUser.UserName,
                    Name = a.AspNetUser.Name,
                    IsApproved = a.IsApproved,
                    ReservationDate = a.ReservationDate,
                    DepatureDate = a.DepatureDate,
                    Number = a.Room.Number,
                    IsAvailable = a.Room.IsAvailable,
                    NumberOfPeople = a.Room.NumberOfPeople,
                    NumberOfBeds = a.Room.NumberOfBeds,
                    IsDeleted = a.IsDeleted,
                    LeavingDate = a.LeavingDate,
                    ComingDate = a.ComingDate
                });
            AutoCompleteStringCollection completeStringCollection = new AutoCompleteStringCollection();
            foreach (var item in resvationData)
            {
                completeStringCollection.Add(item.Name);
            }
            dataGridView1.DataSource = resvationData.ToList();
            

            WindowsFormsUtilities.Helpers.FillText(this.textBox1, completeStringCollection);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectData();
        }

        private void approvedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows != null)
            {
               var selectedresvation = (ReservationInfoView)dataGridView1.SelectedRows[0].DataBoundItem;
              var resvation=  reservationRepository.GetIQueryable().FirstOrDefault(a => a.UserID == selectedresvation.UserID
                && a.ReservationDate == selectedresvation.ReservationDate && a.RoomID == selectedresvation.RoomID);
                resvation.IsApproved = true;
                resvation.IsDeleted = false;
                reservationRepository.Edit(resvation);
                if(reservationRepository.SaveChanges() > 0)
                {
                    reservationRepository.EditAttach(resvation);
                    MessageBox.Show("The Resrvation has been Approved","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    SelectData();
                }
            }
        }

        private void addCommingDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null)
            {
                var selectedresvation = (ReservationInfoView)dataGridView1.SelectedRows[0].DataBoundItem;
                using (SetCommingDate frm = new SetCommingDate(reservationRepository))
                {
                    frm.TypeOfForm = TypeOfForm.Comming;
                    frm.UserInfo = selectedresvation;
                    if(frm.ShowDialog() == DialogResult.OK)
                    {
                        SelectData();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null)
            {
                var selectedresvation = (ReservationInfoView)dataGridView1.SelectedRows[0].DataBoundItem;
                var resvation = reservationRepository.GetIQueryable().FirstOrDefault(a => a.UserID == selectedresvation.UserID
                  && a.ReservationDate == selectedresvation.ReservationDate && a.RoomID == selectedresvation.RoomID);
                resvation.IsApproved = false;
                resvation.IsDeleted = true;
                resvation.Room.IsAvailable = true;
                reservationRepository.Edit(resvation);
                if (reservationRepository.SaveChanges() > 0)
                {
                    MessageBox.Show("The Resrvation has been Deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SelectData();
                }
            }
        }

        private void addLeaveDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null)
            {
                var selectedresvation = (ReservationInfoView)dataGridView1.SelectedRows[0].DataBoundItem;
                using (SetCommingDate frm = new SetCommingDate(reservationRepository))
                {
                    frm.TypeOfForm = TypeOfForm.Leaving;
                    frm.UserInfo = selectedresvation;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        SelectData();
                    }
                }
            }
        }
    }
}
