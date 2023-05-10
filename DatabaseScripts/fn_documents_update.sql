\c findoxdb;

CREATE OR REPLACE FUNCTION public.fn_documents_update(
	in_document_id  	uuid,
	in_file_name  		character varying,
	in_deleted			boolean,
	in_updated_date		timestamp without time zone,
	in_updated_by 		integer)
    RETURNS void
    LANGUAGE SQL
AS $BODY$

	UPDATE public.documents 
	SET file_name = in_file_name,
		deleted = in_deleted,
		updated_date = in_updated_date,
		updated_by = in_updated_by
	WHERE document_id = in_document_id;

$BODY$;

ALTER FUNCTION public.fn_documents_update(uuid, character varying, boolean, timestamp without time zone, integer)
    OWNER TO postgres;
