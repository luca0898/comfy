import styled from 'styled-components';

export const Container = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #2c3e50;
`;

export const Form = styled.form`
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 80px;
  background-color: #34495e;
  border-radius: 5px;
`;

export const Input = styled.input`
  margin-bottom: 10px;
  border:none;
  border-radius: 5px;
  padding: 10px;

  :focus {
    outline: none;
  }
`;

export const Submit = styled.button`
  margin-top: 20px;
  color: white;
  font-size: 14px;
  padding: 10px 30px;
  border: none;
  border-radius: 5px;
  outline: none;
  cursor: pointer;
  background-color: #218c74;
  transition: all 0.3s ease-out;

  :hover {
    background-color: #185f50;
  }
`;
