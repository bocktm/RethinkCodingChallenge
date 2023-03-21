import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { PatientDataSource, Patient } from './patients.datasource';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})

export class PatientsComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<Patient>;
  dataSource?: PatientDataSource;

  displayedColumns = ['firstName', 'lastName', 'birthday', 'gender', 'edit'];
  patientsCSV?: File;

  constructor(private http: HttpClient, private router: Router) {

    this.router.routeReuseStrategy.shouldReuseRoute = () => {
      return false;
    };

    this.http.get<Patient[]>('/api/patients').subscribe(result => {
      this.dataSource = new PatientDataSource(result);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
      this.table.dataSource = this.dataSource;
    }, error => {
      console.error(error);
    });
  }

  ngAfterViewInit(): void {
  }

  fileSelected(event: Event) {
    if (event && event.target) {
      const input:HTMLInputElement = event.target as HTMLInputElement;
      if (input && input.files)
      {
        const file:File = input.files[0];
        if (file) {
          this.patientsCSV = file;
        }
      }
    }
  }

  addPatients(){
    if (this.patientsCSV) {
      const fd = new FormData();
      fd.append("employeeCSVFile", this.patientsCSV);
      this.http.post("/api/patients", fd).subscribe(result => {
        console.log(result);
      }, error => {
        console.error(error);
      });

      this.router.navigateByUrl('/patients');
    }
  }

  replacePatients(){
    if (this.patientsCSV) {
      const fd = new FormData();
      fd.append("employeeCSVFile", this.patientsCSV);
      this.http.put("/api/patients", fd).subscribe(result => {
        console.log(result);
      }, error => {
        console.error(error);
      });

      this.router.navigateByUrl('/patients');
    }
  }
}
