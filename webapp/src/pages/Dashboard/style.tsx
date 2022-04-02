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
  padding: 1rem 2rem;
  background-color: #34495e;
  height: 50px;
  box-shadow: 0 0 5px 0 #161f29;
`;

export const Logo = styled.p`
  color: white;
  font-size: 1.65rem;
  margin: none;
`;

export const NavigationLinks = styled.div`
  display: flex;

  a {
    color: white;
    outline: none;
    text-decoration: none;

    display: block;
    padding: 1rem;
    margin: .4rem;
    border-radius: 4px;
    transition: 300ms ease;

    -webkit-tap-highlight-color: transparent;

    :hover, :active {
      background-color: #2c3e50;
    }
  }

`;
