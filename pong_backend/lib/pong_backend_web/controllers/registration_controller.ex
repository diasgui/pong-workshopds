defmodule PongBackendWeb.RegistrationController do
  use PongBackendWeb, :controller
  alias PongBackend.Repo
  alias PongBackend.Accounts.Player

  def sign_up(conn, %{"name" => name}) do
    changeset = Player.changeset(%Player{}, %{"name" => name, "wins" => 0, "losses" => 0})

    case Repo.insert(changeset) do
      {:ok, player} ->
        conn
        |> put_status(:created)
        |> render("success.json", player: player)
      {:error, changeset} ->
        conn
        |> put_status(:unprocessable_entity)
        |> render(PongBackendWeb.ChangesetView, "error.json", changeset: changeset)
    end
  end
end
