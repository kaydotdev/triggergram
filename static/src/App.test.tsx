import { render, screen } from '@testing-library/react';
import App from './App';

test('renders application logo', () => {
  // arrange
  const altText = "TRIGGERGRAM";

  // act
  render(<App />);

  // assert
  expect(screen.getByAltText(altText)).toBeInTheDocument();
});
