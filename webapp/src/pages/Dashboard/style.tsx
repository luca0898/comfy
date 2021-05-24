import styled from 'styled-components';

export const Container = styled.div`
  background-color: #2c3e50;
  height: 100vh;
  width: 100vw;
`;

export const Header = styled.nav`
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 10px;
  background-color: #34495e;
  height: 50px;
  box-shadow: 0 0 5px 0 #161f29;
`;

export const Logo = styled.p`
  color: white;
  font-size: 26px;
  margin: none;
`;

export const NavigationLinks = styled.div`
  a {
    color: white;
    margin: 0 20px;
    outline: none;
    text-decoration: none;
  }

`;
