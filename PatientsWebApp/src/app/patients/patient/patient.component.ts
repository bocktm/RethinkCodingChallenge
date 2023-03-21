import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Component } from '@angular/core';
import { Patient } from '../patients.datasource';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-patient',
  templateUrl: './patient.component.html',
  styleUrls: ['./patient.component.css']
})
export class PatientComponent {
  patient: Patient = new Patient(0,'','',new Date(),'');

  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute, private formBuilder: FormBuilder)
  {
    this.http.get<Patient>('/api/patient/'+this.route.snapshot.paramMap.get('id')).subscribe(result => {
      this.patient = result;
    }, error => {
      console.error(error);
    });
  }

  updatePatient(){
      console.debug("Update Patient");
      console.debug(this.patient);

      const fd = new FormData();
      fd.append("firstName", this.patient.firstName);
      fd.append("lastName", this.patient.lastName);
      fd.append("birthday", this.patient.birthday.toString());
      fd.append("gender", this.patient.gender);

      this.http.post<Patient>('/api/patient/'+this.route.snapshot.paramMap.get('id'),this.patient).subscribe(result => {
        console.log(result);
      }, error => {
        console.error(error);
      });
      
      this.router.navigateByUrl('/patients');
  }
}
