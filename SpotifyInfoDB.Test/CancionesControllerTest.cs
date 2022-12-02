using AutoMapper;
using Spotify.API.Controllers;
using Spotify.API.Entities;
using Spotify.API.Models;
using Spotify.API.Profiles;
using Spotify.API.Services;
using Castle.Core.Logging;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistInfo.Test
{
    public class CancionesControllerTest
    {
        private readonly ILogger<CancionesController> _logger;
        private readonly CancionesController _cancionesController;
        private readonly Mapper _mapper;

        public CancionesControllerTest()
        {
            //Arrage
            var playlistMock = new Mock<IPlaylistInfoRepository>();
            playlistMock
                .Setup(m => m.GetPlaylistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((new List<Playlist>
                {new Playlist("lista coche"){Description = "fiesta"},
                new Playlist("lista relax"){Description = "meditacioón"},
                new Playlist("concentración"){ Description = "a estudiar"}
                }, new PaginationMetadata(10, 2, 2)));
            playlistMock
                .Setup(m => m.PlaylistExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(true);
            playlistMock
                .Setup(m => m.GetCancionesForPlaylistAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<Cancion>
                {
                    new Cancion("vacaciones"){Description="estopa"},
                    new Cancion("si me vas a elegir"){Description="rosalia"}
                });
            playlistMock
                .Setup(m => m.GetCancionForPlaylistAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(
                    new Cancion("vacaciones"){Description= "estopa", PlaylistId=2,Playlist = new Playlist("lista relax") { Id = 2, Description = "meditacioón" } }
                 );
            playlistMock
                .Setup(m => m.AddCancionForPlaylistAsync(It.IsAny<int>(), It.IsAny<Cancion>()));
            playlistMock
                .Setup(m => m.SaveChangesAsync())
                .ReturnsAsync(
                    true
                 );
            playlistMock
                .Setup(m => m.DeleteCancion(It.IsAny<Cancion>()));
                


            var mapperConfi = new MapperConfiguration(
                cfg => cfg.AddProfile<CancionesProfile>());
            var mapper = new Mapper(mapperConfi);
            _mapper = mapper;
            var logger = Mock.Of<ILogger<CancionesController>>();
            _logger = logger;
            _cancionesController = new CancionesController(logger, playlistMock.Object, mapper);

        }

        [Fact]
        public async Task GetCanciones_Test()
        {
            //Arrage

            //Act

            var result = await _cancionesController.GetCanciones(1);

            //Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<CancionesDto>>>(result);
            var okObject = Assert.IsType<OkObjectResult>(actionResult.Result);
            var dtos = Assert.IsAssignableFrom<IEnumerable<CancionesDto>>(okObject.Value);
            Assert.Equal(2, dtos.Count());


        }

        [Fact]
        public async Task GetCancion_Test()
        {
            //Arrage

            //Act

            var result = await _cancionesController.GetCanciones(2,1);

            //Assert
            var actionResult = Assert.IsType<ActionResult<CancionesDto>>(result);
            var okObject = Assert.IsType<OkObjectResult>(actionResult.Result);
            var dtos = Assert.IsAssignableFrom<CancionesDto>(okObject.Value);
            Assert.NotNull(dtos);   

        }

        [Fact]
        public async Task CreateCancion_Test()
        {
            //Arrage

            //Act
            CancionesForCreationDto cancion = new CancionesForCreationDto()
            {
                Name="Canción Prueba",Description = "prueba"
            } ;

            var result = await _cancionesController.CreateCancion(4, cancion);

            //Assert
            var actionResult = Assert.IsType<ActionResult<CancionesDto>>(result);

        }

        [Fact]
        public async Task UpgradeCancion_Test()
        {
            //Arrage

            //Act
            CancionesForUpdateDto cancion = new CancionesForUpdateDto()
            {
                Name = "ok",
                Description = "prueba"
            };

            var result = await _cancionesController.UpdateCancion(2,2, cancion);

            //Assert
            var actionResult = Assert.IsType<NoContentResult>(result);
            

        }

        /*[Fact]
        public async Task PartialUpgradeCancion_Test()
        {
            //Arrage

            JsonPatchDocument<CancionUpdateDto> cancion = new JsonPatchDocument<CancionUpdateDto>();
            cancion.Replace(e => e.Nombre, "nuebo");
            cancion.Replace(e => e.Description, null);


            //Act

            var result = await _cancionesController.PartiallyUpgrateCancion(2, 2, cancion);

            //Assert
            var actionResult = Assert.IsType<NoContentResult>(result);


        }*/


        [Fact]
        public async Task DeleteCancion_Test()
        {
            //Arrage

            //Act

            var result = await _cancionesController.DeleteCancion(2, 2);

            //Assert
            var actionResult = Assert.IsType<NoContentResult>(result);


        }
    }
}
