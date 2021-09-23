import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '../_models/Evento';
import { EventoService } from '../_services/evento.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { templateJitUrl } from '@angular/compiler';
import { ToastrService } from 'ngx-toastr';

defineLocale('pt-pt', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  
  titulo = 'Eventos';

  eventosFiltrados: Evento[] = [];
  eventos: Evento[] = [];

  evento!: Evento;
  modoSalvar = 'post';
  dataEvento= '';

  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  registerForm: FormGroup = new FormGroup({});
  bodyEliminarEvento = '';

  file!: File;

  _filtroLista='';
  fileNameToUpdate: string = '';
  dataAtual: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this._filtroLista != '' ? this.filtrarEventos(this._filtroLista) : this.eventos;
  }

  openModal(template: any){
    this.registerForm.reset();
    template.show();
  }

  novoEvento(template: any){
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  excluirEvento(evento: Evento, template: any) {
    template.show();
    this.evento = evento;
    this.bodyEliminarEvento = `Tem a certeza que deseja eliminar o Evento ${evento.tema}?`
  }

  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
        template.hide();
        this.getEventos();
        this.toastr.success('Eliminado com sucesso');
      }, error => {
        console.log(error);
        this.toastr.error('Erro ao tentar eliminar');
      }
    );
  }

  editarEvento(template: any, evento: Evento){
    this.modoSalvar = 'put';
    this.openModal(template);
    this.evento = evento;
    this.fileNameToUpdate = evento.imagemURL.toString();
    this.registerForm.patchValue(evento);
  }

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private toastr: ToastrService
    ) { 
      this.localeService.use('pt-pt');
    }

  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      ( evento: { tema: string; }) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem() {
    this.mostrarImagem = ! this.mostrarImagem;
  }

  uploadImagem() {
    if (this.modoSalvar === 'post') {
      this.evento.imagemURL = this.file.name;
    } else {
      this.evento.imagemURL = this.fileNameToUpdate;
    }
    this.eventoService.postUpload(this.file, this.evento.imagemURL)
      .subscribe(
        () => {
          this.dataAtual = new Date().getMilliseconds().toString();
          this.getEventos();
        }
      );
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {
        this.evento = Object.assign({}, this.registerForm.value);

        this.uploadImagem();
        
        this.eventoService.postEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            template.hide();
            this.getEventos();
            this.toastr.success('Inserido com sucesso');
          }, error => {
            console.log(error);
            this.toastr.error('Erro ao tentar inserir');
          }
        );
      } else {
        console.log('put...');
        this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);

        this.uploadImagem();        

        this.eventoService.putEvento(this.evento).subscribe(
          () => {
            template.hide();
            this.getEventos();
            this.toastr.success('Gravado com sucesso');
          }, error => {
            console.log(error);
            this.toastr.error('Erro ao tentar gravar');
          }
        );
      }
    } else {
      console.log('Form inválida');
    }
  }

  onFileChange(event: any) {
    const reader = new FileReader();

    if (event.target.files && event.target.files.length) {
      this.file = event.target.files[0];
      console.log(this.file);
    }
  }

  validation() {
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['',Validators.required],
      dataEvento: ['',Validators.required],
      imagemUrl: ['',Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.min(1), Validators.max(120000)]],
      telefone: ['',Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  getEventos(){
    this.dataAtual = new Date().getMilliseconds().toString();
    this.eventoService.getAllEvento().subscribe(
      (_eventos: Evento[]) => {
        this.eventos = _eventos;
        this.eventosFiltrados = this.eventos;
        console.log(_eventos);
      }, error => {
        console.log(error);
        this.toastr.error('Erro ao carregar os eventos!');
      }
    );
  }

}
