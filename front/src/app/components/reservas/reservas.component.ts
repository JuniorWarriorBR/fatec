import { Component, OnInit } from '@angular/core';
import { ReservaService } from 'src/app/services/reserva.service';
import { Reserva } from 'src/app/models/reserva/reserva';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-reservas',
  templateUrl: './reservas.component.html',
  styleUrls: ['./reservas.component.css'],
})
export class ReservasComponent implements OnInit {
  reserva = {} as Reserva;
  reservas: Reserva[];

  constructor(
    private reservaService: ReservaService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.getReserva();
  }

  // defini se uma reserva será criada ou atualizada
  saveReserva(form: NgForm) {
    if (this.reserva.id !== undefined) {
      this.reserva.concluida = true;
      this.reservaService.updateReserva(this.reserva).subscribe(() => {
        this.getReserva();
      });
    } else {
      this.reservaService.saveReserva(this.reserva).subscribe(() => {
        this.getReserva();
      });
    }
  }

  // defini se uma reserva será criada ou atualizada
  updateReserva(id) {
    this.reserva.id = id;
    this.reserva.concluida = true;
    this.reservaService.updateReserva(this.reserva).subscribe(() => {
      this.getReserva();
      this.toastr.success('Agendamento encerrado com sucesso!');
    });
  }

  getReserva() {
    this.reservaService.getReserva().subscribe((reservas: Reserva[]) => {
      this.reservas = reservas;
    });
  }

  // Editar reserva
  editReserva(reserva: Reserva) {
    this.reserva = { ...reserva };
  }

  // limpa o formulario
  cleanForm(form: NgForm) {
    this.getReserva();
    form.resetForm();
    this.reserva = {} as Reserva;
  }
}
