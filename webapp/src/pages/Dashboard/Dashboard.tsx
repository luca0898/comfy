import React from 'react';
import {
  Container, Header, Logo, NavigationLinks,
} from './style';

const DashboardScreen : React.FC = () => (
  <Container>

    <Header>
      <Logo>Comfy</Logo>
      <NavigationLinks>
        <a href="/">Home</a>
        <a href="/">Perfil</a>
        <a href="/">Sobre</a>
      </NavigationLinks>

    </Header>
  </Container>
);

export default DashboardScreen;
