import { CssBaseline } from '@material-ui/core';
import {
  createTheme,
  ThemeProvider
} from '@material-ui/core/styles';
import { indigo, blue } from '@material-ui/core/colors';
import NavBar from './components/NavBar';
import SearchBar from './components/SearchBar';


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
      <SearchBar />
    </ThemeProvider>
  );
}

export default App;
