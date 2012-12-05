using System;
using System.Collections.Generic;
using RestEntities;
using RestEntities.Respuestas;
using Rest.Services.Model;
using System.Linq;


namespace Rest.Services
{
    public class DoctorService : IDoctorService
    {
        #region IDoctorService Members

        public RespuestaRegistro Register(Doctor doctor)
        {
            var _context = new PruebaEntities();
            var doctorExistente = (from d in _context.doctor where d.usuario.usuario1 == doctor.usuario select d).FirstOrDefault();

            if (doctorExistente != null)
            {
                return new RespuestaRegistro() { Status = 0, DoctorId = -1, Message=String.Format("El usuario '{0}' ya se encuentra registrado",doctor.usuario)};
            }

            doctor doctorEntity = DoctorEntityFromDoctor(doctor);
            
            RespuestaRegistro respuesta = new RespuestaRegistro() { Status = 0, DoctorId = -1 };

            try
            {
                _context.doctor.AddObject(doctorEntity);
                _context.SaveChanges();
                respuesta.DoctorId = doctorEntity.id;
                respuesta.Status = 200;
                respuesta.Message = "Registro exitoso";

            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;
        }

        public Doctor GetInfo(string id)
        {
            var _context = new PruebaEntities();
            int doctorId = 0;

            Int32.TryParse(id, out doctorId);

            if(doctorId>0){
                doctor doctor = _context.doctor.FirstOrDefault(d=>d.id==doctorId);
                return DoctorFromDoctorEntity(doctor);
            }

            return null;
        }

        public RespuestaRegistro Update(Doctor doctor)
        {
            var _context = new PruebaEntities();
            string error = "No se encontró el doctor";

            try
            {
                var doctorExistente = _context.doctor.FirstOrDefault(d=>d.id == doctor.id);

                if (doctorExistente != null)
                {
                    doctorExistente.anio_residencia = doctor.anio_residencia;
                    doctorExistente.nombre = doctor.nombre;
                    doctorExistente.apellido = doctor.apellido;
                    doctorExistente.fecha_nac = Convert.ToDateTime(doctor.fecha_nac);
                    doctorExistente.anio_residencia = doctor.anio_residencia;
                    doctorExistente.pais_id = doctor.pais_id;
                    doctorExistente.estado_id = doctor.estado_id;
                    doctorExistente.ciudad_id = doctor.ciudad_id;
                    doctorExistente.consulta_institucion = doctor.consulta_institucion;
                    doctorExistente.consulta_privada = doctor.consulta_privada;
                    doctorExistente.especialidad_id = doctor.especialidad_id;
                    doctorExistente.genero = doctor.genero;
                    doctorExistente.institucion = doctor.institucion;
                    doctorExistente.residente = doctor.residente;
                    doctorExistente.subespecialidad_id = doctor.subespecialidad_id;
                    doctorExistente.telefono = doctor.telefono;

                    _context.SaveChanges();

                    return new RespuestaRegistro() { DoctorId = doctor.id, Message = "Actualizacion correcta", Status = 1 };
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return new RespuestaRegistro() { DoctorId = doctor.id, Message = error, Status = 0 };
        }

        public RespuestaActivacion Activate(string codigoStr, string pais)
        {
            var _context = new PruebaEntities();

            RespuestaActivacion respuesta = new RespuestaActivacion() { Activated = false, Message = "" }; ;
            int paisId = 0;

            Int32.TryParse(pais, out paisId);

            codigo codigo = (from c in _context.codigo where c.codigo1 == codigoStr && c.pais_id == paisId && c.habilitado == 0 select c).FirstOrDefault();

            if (codigo != null)
            {
                try
                {
                    codigo.habilitado = 1;
                    _context.SaveChanges();
                    respuesta.Activated = true;
                    respuesta.Message = "Activacion exitosa";
                }
                catch (Exception ex)
                {
                    respuesta.Message = ex.Message;
                }
            }
            else
            {
                respuesta.Message = "Activación fallida";
            }

            return respuesta;
        }

        public RespuestaLogin Login(Login login)
        {
            var _context = new PruebaEntities();

            RespuestaLogin respuesta = new RespuestaLogin() { Status = 0, DoctorId = -1 };

            doctor doc = (from d in _context.doctor where d.usuario.usuario1 == login.Usuario && d.usuario.password == login.Password select d).FirstOrDefault();

            if (doc != null)
            {
                respuesta.Status = 1;
                respuesta.Message = "Login exitoso";
                respuesta.DoctorId = doc.id;
                respuesta.DoctorName = doc.nombre;
            }
            else
            {
                respuesta.Status = 0;
                respuesta.Message = "Nombre de usuario y contraseña no validos";
            }


            return respuesta;
        }

        public RespuestaRegistro LoginG(string mail, string password)
        {
            var _context = new PruebaEntities();

            RespuestaRegistro respuesta = new RespuestaRegistro() { Status = 0, DoctorId = -1 };

            doctor doc = (from d in _context.doctor where d.usuario.usuario1 == mail && d.usuario.password == password select d).FirstOrDefault();

            if (doc != null)
            {
                respuesta.Status = 1;
                respuesta.Message = "Login exitoso";
                respuesta.DoctorId = doc.id;
            }
            else
            {
                respuesta.Status = 0;
                respuesta.Message = "Nombre de usuario y contraseña no validos";
            }


            return respuesta;
        }

        public List<Paciente> ObtenPacientes(string id)
        {
            var _context = new PruebaEntities();
            List<Paciente> pacientes = null;

            int doctorId = Convert.ToInt32(id);

            foreach (paciente pac in _context.paciente)
            {
                if (pacientes == null)
                {
                    pacientes = new List<Paciente>();
                }

                pacientes.Add(PacienteFromPacienteEntity(pac));
            }

            return pacientes;
        }

        public RespuestaRegistroContacto AgregaContacto(Paciente paciente)
        {
            var pacienteEntity = PacienteEntityFromPaciente(paciente);
            var _context = new PruebaEntities();
            RespuestaRegistroContacto respuesta = new RespuestaRegistroContacto() { ContactId = -1, Status = 0, Message = "" };

            try
            {
                _context.paciente.AddObject(pacienteEntity);
                _context.SaveChanges();

                respuesta.ContactId = pacienteEntity.id;
                respuesta.Message = "Registro de contacto exitoso";
                respuesta.Status = 1;
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;
        }

        public int ActualizaContacto(Paciente paciente)
        {
            var _context = new PruebaEntities();

            paciente pacienteOld = (from p in _context.paciente where p.id == paciente.id select p).SingleOrDefault();

            pacienteOld.calle = paciente.calle;

            return _context.SaveChanges();
        }

        //public RespuestaRegistroMedida AgregaMedida(Medida medidaRest)
        //{
        //    RespuestaRegistroMedida respuesta = new RespuestaRegistroMedida() { MedidaId = -1, Message = "", Status = -1 };
        //    medida medidaEntity = new medida();
        //    var _context = new PruebaEntities();

        //    try
        //    {

        //        medidaEntity.edad = medidaRest.edad;
        //        medidaEntity.fecha_medicion = DateTime.Now;
        //        medidaEntity.paciente_id = medidaRest.paciente_id;
        //        medidaEntity.peso = medidaRest.peso;
        //        medidaEntity.talla = medidaRest.talla;

        //        _context.medida.Add(medidaEntity);
        //        _context.SaveChanges();

        //        respuesta.MedidaId = medidaEntity.id;
        //        respuesta.Status = 200;

        //    }catch(Exception ex){
        //        respuesta.Message = ex.Message;
        //    }

        //    return respuesta;
        //}

        //public List<Medida> ListaMedidas(string paciente_id)
        //{
        //    var _context = new PruebaEntities();
        //    int pacienteId = 0;
        //    List<Medida> resultado = null;

        //    Int32.TryParse(paciente_id, out pacienteId);

        //    if (pacienteId > 0)
        //    {
        //        var listaMedidas = (from m in _context.medida where m.paciente_id==pacienteId orderby m.fecha_medicion select m).ToList();

        //        foreach(MedidaEntity medidaEntity in listaMedidas){
        //            if (resultado == null)
        //            {
        //                resultado = new List<Medida>();
        //            }
        //            resultado.Add(new Medida(){ id = medidaEntity.id, paciente_id=medidaEntity.paciente_id, edad=medidaEntity.edad, peso=medidaEntity.peso, talla=medidaEntity.talla});
        //        }
        //    }

        //    return resultado;
        //}


        #endregion

        #region metodos conversion clases
        private Doctor DoctorFromDoctorEntity(doctor doctorEntity)
        {
            Doctor doctor = new Doctor();

            doctor.id = doctorEntity.id;
            doctor.usuario = doctorEntity.usuario.usuario1;
            doctor.nombre = doctorEntity.nombre;
            doctor.apellido = doctorEntity.apellido;
            doctor.fecha_nac = doctorEntity.fecha_nac.ToShortDateString();
            doctor.anio_residencia = doctorEntity.anio_residencia;
            doctor.pais_id = doctorEntity.pais_id;
            doctor.estado_id = doctorEntity.estado_id;
            doctor.ciudad_id = doctorEntity.ciudad_id;
            doctor.consulta_institucion = doctorEntity.consulta_institucion;
            doctor.consulta_privada = doctorEntity.consulta_privada;
            doctor.especialidad_id = doctorEntity.especialidad_id;
            doctor.genero = doctorEntity.genero;
            doctor.institucion = doctorEntity.institucion;
            doctor.residente = doctorEntity.residente;
            doctor.password = doctorEntity.usuario.password;
            doctor.subespecialidad_id = doctorEntity.subespecialidad_id;
            doctor.telefono = doctorEntity.telefono;

            return doctor;
        }

        private doctor DoctorEntityFromDoctor(Doctor doctor)
        {
            doctor doctorEntity = new doctor();

            doctorEntity.id = doctor.id;
            doctorEntity.usuario.usuario1 = doctor.usuario;
            doctorEntity.usuario.password = doctor.password;
            doctorEntity.nombre = doctor.nombre;
            doctorEntity.apellido = doctor.apellido;
            doctorEntity.fecha_nac = Convert.ToDateTime(doctor.fecha_nac);
            doctorEntity.anio_residencia = doctor.anio_residencia;
            doctorEntity.pais_id = doctor.pais_id;
            doctorEntity.estado_id = doctor.estado_id;
            doctorEntity.ciudad_id = doctor.ciudad_id;
            doctorEntity.consulta_institucion = doctor.consulta_institucion;
            doctorEntity.consulta_privada = doctor.consulta_privada;
            doctorEntity.especialidad_id = doctor.especialidad_id;
            doctorEntity.genero = doctor.genero;
            doctorEntity.institucion = doctor.institucion;
            doctorEntity.residente = doctor.residente;
            doctorEntity.subespecialidad_id = doctor.subespecialidad_id;
            doctorEntity.telefono = doctor.telefono;

            return doctorEntity;
        }

        private Paciente PacienteFromPacienteEntity(paciente pacienteEntity)
        {
            Paciente paciente = new Paciente();

            paciente.alergico = pacienteEntity.alergico;
            paciente.calle = pacienteEntity.calle;
            paciente.ciudad = pacienteEntity.ciudad;
            paciente.ciudad_nac = pacienteEntity.ciudad_nac;
            paciente.colonia = pacienteEntity.colonia;
            paciente.correo = pacienteEntity.correo;
            paciente.cp = pacienteEntity.cp.ToString();
            paciente.doctor_id = pacienteEntity.doctor_id;
            paciente.estado = pacienteEntity.estado;
            paciente.fecha_nac = pacienteEntity.fecha_nac.ToShortDateString();
            paciente.grupo_sanguineo = pacienteEntity.grupo_sanguineo;
            paciente.id = pacienteEntity.id;
            paciente.lugar_nac = pacienteEntity.lugar_nac;
            paciente.nombre = pacienteEntity.nombre;
            paciente.nombre_encargado = pacienteEntity.nombre_encargado;
            paciente.nombre_madre = pacienteEntity.nombre_madre;
            paciente.nombre_padre = pacienteEntity.nombre_padre;
            paciente.notas = pacienteEntity.notas;
            paciente.ocupacion_madre = pacienteEntity.ocupacion_madre;
            paciente.ocupacion_padre = pacienteEntity.ocupacion_padre;
            paciente.rh = pacienteEntity.rh;
            paciente.sexo = pacienteEntity.sexo;
            paciente.telefono = pacienteEntity.telefono;
            paciente.telefono_encargado = pacienteEntity.telefono_encargado;

            return paciente;
        }

        private paciente PacienteEntityFromPaciente(Paciente pacienteEntity)
        {
            paciente paciente = new paciente();

            paciente.alergico = pacienteEntity.alergico;
            paciente.calle = pacienteEntity.calle;
            paciente.ciudad = pacienteEntity.ciudad;
            paciente.ciudad_nac = pacienteEntity.ciudad_nac;
            paciente.colonia = pacienteEntity.colonia;
            paciente.correo = pacienteEntity.correo;
            paciente.cp = Convert.ToInt32(pacienteEntity.cp);
            paciente.doctor_id = pacienteEntity.doctor_id;
            paciente.estado = pacienteEntity.estado;
            paciente.fecha_nac = Convert.ToDateTime(pacienteEntity.fecha_nac);
            paciente.grupo_sanguineo = pacienteEntity.grupo_sanguineo;
            paciente.id = pacienteEntity.id;
            paciente.lugar_nac = pacienteEntity.lugar_nac;
            paciente.nombre = pacienteEntity.nombre;
            paciente.nombre_encargado = pacienteEntity.nombre_encargado;
            paciente.nombre_madre = pacienteEntity.nombre_madre;
            paciente.nombre_padre = pacienteEntity.nombre_padre;
            paciente.notas = pacienteEntity.notas;
            paciente.ocupacion_madre = pacienteEntity.ocupacion_madre;
            paciente.ocupacion_padre = pacienteEntity.ocupacion_padre;
            paciente.rh = pacienteEntity.rh;
            paciente.sexo = pacienteEntity.sexo;
            paciente.telefono = pacienteEntity.telefono;
            paciente.telefono_encargado = pacienteEntity.telefono_encargado;

            return paciente;
        }
    #endregion
    }
}