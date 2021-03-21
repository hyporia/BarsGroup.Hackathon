import { Button, FormControl } from '@material-ui/core';
import React, { useRef } from 'react';

/**
 * Кнопка загрузки файла
 */
const FileUploadButton = ({
    text, onFileSelected, margin
}) => {
    const fileInput = useRef(null);

    const onClick = () => {
        fileInput.current.click();
    }

    const onChange = (file) => {
        onFileSelected && onFileSelected(file);
    }

    return (
        <FormControl>
            <input type="file" style={{ display: 'none' }} accept=".jpg,.png,.pdf,.doc,.xls" ref={fileInput} onChange={e => {
                onChange(e?.currentTarget?.files && e.currentTarget.files[0]);
            }} />

            <Button variant="contained"
                style={{ margin }}
                onClick={() => onClick()}>{text}</Button>
        </FormControl >
    );
};

export default React.memo(FileUploadButton);
