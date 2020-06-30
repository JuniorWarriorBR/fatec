import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/models/usuario/usuario';
import { RegistraUsuarioService } from 'src/app/services/registra-usuario.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-registrar',
  templateUrl: './registrar.component.html',
  styleUrls: ['./registrar.component.css'],
})
export class RegistrarComponent implements OnInit {
  usuario = {} as Usuario;

  constructor(
    private registraUsuarioService: RegistraUsuarioService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  // defini se uma usuario será criada ou atualizada
  saveUsuario(form: NgForm) {
    if (this.usuario.id !== undefined) {
      this.registraUsuarioService.updateUsuario(this.usuario).subscribe(() => {
        // this.cleanForm(form);
      });
    } else {
      this.registraUsuarioService.saveUsuario(this.usuario).subscribe((res) => {
        this.router.navigate(['/login']);
      });
    }
  }
}
