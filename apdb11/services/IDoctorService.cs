using System.Collections;
using System.Collections.Generic;
using apdb11.models;

namespace apdb11.services
{
    public interface IDoctorService
    {
        public IEnumerable<DoctorResponse> GetDoctors();
        public bool ModifyDoctor(DoctorRequest request);
        public bool DeleteDoctor(int id);
        public bool AddDoctor(DoctorRequest request);
        public void PopulateData();
    }
}