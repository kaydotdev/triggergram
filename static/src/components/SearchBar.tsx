import { useState } from 'react';
import {
    createStyles,
    makeStyles,
    Theme
} from '@material-ui/core/styles';
import {
    InputLabel,
    OutlinedInput,
    FormControl
} from '@material-ui/core';
import {
    Search as SearchIcon
} from '@material-ui/icons';


const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        margin: {
            margin: theme.spacing(3),
            width: "calc(100% - 48px)"
        },
        icon: {
            marginRight: '10px'
        },
        textArea: {
            borderRadius: '10px'
        }
    }),
);


function SearchBar() {
    const classes = useStyles();
    const [query, setQuery] = useState('');

    return (
        <FormControl className={classes.margin} variant="outlined">
            <InputLabel htmlFor="search-bar">Search</InputLabel>
            <OutlinedInput
                id="search-bar"
                className={classes.textArea}
                value={query}
                onChange={(event) => setQuery(event.target.value)}
                startAdornment={<SearchIcon className={classes.icon} />}
                labelWidth={50}
            />
        </FormControl>
    );
}

export default SearchBar;
