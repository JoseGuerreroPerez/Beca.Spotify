using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Spotify.API.Services;
using Spotify.API.Entities;
using Spotify.API.Controllers;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace PlaylistInfo.Test
{
    public class PlaylistControllerTest
    {
        private readonly PlaylistsController _playlistController;
        public PlaylistControllerTest()
        {
            //Arrage
            var playlistMock = new Mock<IPlaylistInfoRepository>();
            playlistMock
                .Setup(m => m.GetPlaylistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((new List<Playlist>
                {new Playlist("lista coche"){Description = "fiesta"},
                new Playlist("lista relax"){Description = "meditacioón"},
                new Playlist("concentración"){ Description = "a estudiar"}
                }, null));
            var mapperConfiguration = new MapperConfiguration(
                cfg => cfg.AddProfile<Spotify.API.Profiles.PlaylistProfile>());
            var mapper = new Mapper(mapperConfiguration);
            var logger = Mock.Of<ILogger<Playlist>>();
            _playlistController = new PlaylistsController(playlistMock.Object, mapper);
        }

        [Fact]
        public async Task GetPlaylist_model()
        {
            //arrange   
            //Act
            var result = await _playlistController.GetPlaylists("lista coche", "", 1, 1);

            //Assert.
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Spotify.API.Models.PlaylistWithoutCancionesDto>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetPlaylist_modelDto()
        {
            //arrange   
            //Act
            var result = await _playlistController.GetPlaylists("lista coche", "", 1, 1);

            //Assert.
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Spotify.API.Models.PlaylistWithoutCancionesDto>>>(result);
            Assert.IsAssignableFrom<IEnumerable<Spotify.API.Models.PlaylistWithoutCancionesDto>>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public async Task GetPlaylists_ControllerTest()
        {
            //arrange   
            //Act

            var result = await _playlistController.GetPlaylists("lista coche", "", 1, 1);

            //Assert.
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Spotify.API.Models.PlaylistWithoutCancionesDto>>>(result);
            var okObject = Assert.IsType<OkObjectResult>(actionResult.Result);
            var dtos = Assert.IsAssignableFrom<IEnumerable<Spotify.API.Models.PlaylistWithoutCancionesDto>>(okObject.Value);
            Assert.Equal(3, dtos.Count());
        }

        public async Task GetPlaylist_ControllerTest()
        {
            //Arrage

            /*var playlistMock = new Mock<IPlaylistRepository>();
            playlistMock
                .Setup(m => m.GetPlaylistAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(
                new Playlist("linking park")
                );
            var mapperConfiguration = new MapperConfiguration(
                cfg => cfg.AddProfile<Spotify.API.Profiles.PlaylistProfile>());
            var mapper = new Mapper(mapperConfiguration);
            var logger = Mock.Of<ILogger<Playlist>>();
            var playlistController = new PlaylistsController(logger, playlistMock.Object, mapper);
            //Act
            */
            var result = await _playlistController.GetPlaylist(5, false);

            //Assert
            var okOkject = Assert.IsType<OkObjectResult>(result);
            var dtos = Assert.IsAssignableFrom<IEnumerable<Spotify.API.Models.PlaylistWithoutCancionesDto>>(okOkject.Value);
            Assert.Single(dtos);


        }


    }
}

