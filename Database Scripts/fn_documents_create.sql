CREATE OR REPLACE FUNCTION public.fn_documents_create(
	in_document_id  	uuid,
	in_file_name  		character varying,
	in_file_length  	bigint,
	in_deleted			boolean,
	in_created_date		timestamp without time zone,
	in_created_by 		integer)
    RETURNS uuid
    LANGUAGE SQL
AS $BODY$

	INSERT INTO public.documents(document_id, file_name, file_length, deleted, created_date, created_by, updated_date, updated_by)
	VALUES (in_document_id, in_file_name, in_file_length, in_deleted, in_created_date, in_created_by, in_created_date, in_created_by)
	RETURNING document_id;

$BODY$;

ALTER FUNCTION public.fn_documents_create(uuid, character varying, bigint, boolean, timestamp without time zone, integer)
    OWNER TO postgres;
