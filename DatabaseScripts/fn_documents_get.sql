\c findoxdb;

CREATE OR REPLACE FUNCTION public.fn_documents_get(
	in_document_id uuid)
    RETURNS documents
    LANGUAGE 'sql'
AS $BODY$

	SELECT 
		*
	FROM documents
	WHERE document_id = in_document_id
	
$BODY$;

ALTER FUNCTION public.fn_documents_get(uuid)
    OWNER TO postgres;
