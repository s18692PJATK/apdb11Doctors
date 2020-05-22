using System;
using System.Collections.Generic;
using System.Linq;
using apdb11.models;

namespace apdb11.services
{
    public class DoctorServiceImpl : IDoctorService
    {
        private readonly DoctorDbContext _context;

        public DoctorServiceImpl(DoctorDbContext context)
        {
            _context = context;
        }
        public IEnumerable<DoctorResponse> GetDoctors()
        {
            var doctorEntries = _context.Doctors;
            var doctors = doctorEntries.Select(d => new DoctorResponse
            {
                IdDoctor = d.IdDoctor,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email
            }).ToList();
            return doctors;
        }

        public bool ModifyDoctor(DoctorRequest request)
        {
            var updatedRequest = UpdateDoctor(request);
            try
            {
                _context.Update(updatedRequest);
            }
            catch(Exception)
            {
                return false;
            }

            return true;

        }

        private Doctor UpdateDoctor(DoctorRequest request)
        {
            var doctorEntry = _context.Doctors
                .First(d => d.IdDoctor == request.Id);
            var updatedDoctor = new Doctor
            {
                IdDoctor = doctorEntry.IdDoctor,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Prescriptions = doctorEntry.Prescriptions
            };
            return updatedDoctor;
        }

        public bool DeleteDoctor(int id)
        {
            var doctorEntry = _context.Doctors.First(d => d.IdDoctor == id);
            try
            {
                _context.Doctors.Remove(doctorEntry);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool AddDoctor(DoctorRequest request)
        {
            var newDoctor = new Doctor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };
            try
            {
                _context.Doctors.Add(newDoctor);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public void PopulateData()
        {
            var doctor = new Doctor
            {
                FirstName = "Edzio",
                LastName = "Listonosz",
                Email = "edzio@pedzio.com",
            };
            var doctorEntry = _context.Doctors
                .Where(d => d.FirstName == doctor.FirstName)
                .Where(d => d.LastName == doctor.LastName)
                .First(d => d.Email == doctor.Email);
            
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            var patient = new Patient
            {
                FirstName = "Arnold",
                LastName = "Boczek",
                BirthDate = DateTime.Now
            };
            _context.Patients.Add(patient);
            _context.SaveChanges();
            var patientEntry = _context.Patients
                .Where(d => d.FirstName == patient.FirstName)
                .Where(d => d.LastName == patient.LastName)
                .First(d => d.BirthDate.Equals(patient.BirthDate));

            var medicament = new Medicament
            {
                Name = "Xanax",
                Description = "some description",
                Type = "some type"
            };
            _context.Medicaments.Add(medicament);
            _context.SaveChanges();
            var medicamentEntry = _context.Medicaments
                .Where(m => m.Name == medicament.Name)
                .Where(m => m.Description == medicament.Description)
                .First(m => m.Type == medicament.Type);
            var prescription = new Prescription
            {
                IdDoctor = doctorEntry.IdDoctor,
                IdPatient = patientEntry.IdPatient,
                Date = DateTime.Now,
                Doctor = doctorEntry,
                Patient = patientEntry,
                DueDate = DateTime.Now,

            };
            _context.Prescriptions.Add(prescription);
            _context.SaveChanges();
            var prescriptionEntry = _context.Prescriptions
                .Where(p => p.Date.Equals( prescription.Date))
                .Where(p => p.Doctor == prescription.Doctor)
                .Where(p => p.Patient == prescription.Patient)
                .Where(p => p.DueDate.Equals(prescription.DueDate))
                .Where(p => p.IdDoctor == prescription.IdDoctor)
                .First(p => p.IdPatient == prescription.IdPatient);
            var pres_medicament = new PrescriptionMedicament
            {
                IdMedicament = medicamentEntry.IdMedicament,
                IdPrescription = prescriptionEntry.IdPrescription,
                Details = "some details",
                Dose = 1,
                Medicament = medicamentEntry,
                Prescription = prescriptionEntry
            };
            _context.PrescriptionMedicaments.Add(pres_medicament);
            _context.SaveChanges();
        }
    }
}