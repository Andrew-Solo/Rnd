import React from 'react';
import { Outlet } from 'react-router-dom';
import {Container, Content, Header, Nav, Sidebar, Sidenav} from "rsuite";

function Root() {
  return (
    <Container>
      <Sidebar>
        <Sidenav.Header>
          Header
        </Sidenav.Header>
        <Sidenav.Body>
          <Nav>
            <Nav.Item>Dashboard</Nav.Item>
            <Nav.Item>Games</Nav.Item>
          </Nav>
        </Sidenav.Body>
      </Sidebar>
      <Container>
        <Header/>
        <Content>
          <Outlet/>
        </Content>
      </Container>
    </Container>
  );
}

export default Root;
