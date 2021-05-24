import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useState } from 'react';
import {
  Container, Form, Input, Submit,
} from './style';

const LoginScreen : React.FC = () => {
  const [isLoading, setIsLoading] = useState(false);

  const submitLoginForm = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    setIsLoading(true);
    setTimeout(() => {
      setIsLoading(false);
    }, 1000);
  };

  return (
    <Container>
      <Form onSubmit={(e) => submitLoginForm(e)}>
        <Input type="text" name="username" id="username" placeholder="User Name" minLength={3} required />
        <Input type="password" name="password" id="password" placeholder="Password" minLength={3} required />

        <Submit disabled={isLoading} type="submit">
          {!isLoading ? <>Acessar</> : <FontAwesomeIcon icon={faSpinner} spin />}
        </Submit>
      </Form>
    </Container>
  );
};

export default LoginScreen;
