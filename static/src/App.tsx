import { CssBaseline } from '@material-ui/core';
import {
  createTheme,
  ThemeProvider
} from '@material-ui/core/styles';
import { indigo, blue } from '@material-ui/core/colors';
import NavBar from './navbar/NavBar';


const theme = createTheme({
  palette: {
    type: 'light',
    primary: indigo,
    secondary: blue
  }
});


function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline/>
      <NavBar />
    </ThemeProvider>
  );
}

export default App;
